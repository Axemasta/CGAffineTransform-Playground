using CoreFoundation;
using CoreGraphics;
using Foundation;
using System;
using System.Diagnostics;
using UIKit;
using Vision;

namespace CGAffinePlayground
{
    public partial class ViewController : UIViewController
    {
        private const string _failedMessageTemplate = @"The {0} was not converted from native parameters successfully.
Expected transformation:
{1}
Actual transformation:
{2}
";

        private static CGAffineTransform _expectedTransform = new CGAffineTransform(xx: 1341.0851328f, yx: 0.0f,
                xy: 0.0f, yy: 887.743584f,
                x0: 1173.65472f, y0: 1046.13768f);

        public ViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            PrintTransformation();
        }

        private void PrintTransformation()
        {
            var displaySize = new CGSize(4032, 3024);

            var faceObservationBoundingBox = new CGRect(0.291085f, 0.345945f, 0.3326104f, 0.293566f);

            var faceBounds = VNUtils.GetImageRect(faceObservationBoundingBox, Convert.ToUInt32(displaySize.Width), Convert.ToUInt32(displaySize.Height));

            var affineTransform = GetAffineTransform(faceBounds.Location.X, faceBounds.Location.Y, faceBounds.Size.Width, faceBounds.Size.Height);

            var transformString = affineTransform.ToPreciseString();

            var correctTransform = EvaluateAffineTransformationString(affineTransform);

            DisplayResult(correctTransform, transformString);
        }

        private bool EvaluateAffineTransformationString(CGAffineTransform affineTransform)
        {
            // Swift Prints:
            // CGAffineTransform(a: 1341.0851328, b: 0.0, c: 0.0, d: 887.743584, tx: 1173.65472, ty: 1046.13768)

            // Xamarin Prints:
            // xx:0.0 yx:0.0 xy:0.0 yy:0.0 x0:2473224.99 y0:1322742.4

            return affineTransform.ToString() == _expectedTransform.ToString();
        }

        private void DisplayResult(bool success, string actualTransform)
        {
            var title = success ? "Success" : "Failure";

            var message = success ?
                $"The {nameof(CGAffineTransform)} was successfully reproduced from native api" :
                string.Format(_failedMessageTemplate, nameof(CGAffineTransform), _expectedTransform.ToString(), actualTransform);

            var alertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

            DispatchQueue.MainQueue.DispatchAsync(() => PresentViewController(alertController, true, null));
        }

        private CGAffineTransform GetAffineTransform(nfloat x, nfloat y, nfloat width, nfloat height)
        {
            CGAffineTransform affineTransform;

            // Original Attempt
            //affineTransform = new CGAffineTransform();
            //affineTransform.Translate(x, y);
            //affineTransform.Scale(width, height);

            // Initialized with correct values (to check eval code works)
            //affineTransform = new CGAffineTransform(xx: 1341.0851328f, yx:0.0f,
            //    xy:0.0f, yy: 887.743584f,
            //    x0: 1173.65472f, y0:1046.13768f);

            // Attempt 2 - Translations ok but scale is still crazy
            //affineTransform = CGAffineTransform.MakeTranslation(x, y);
            //affineTransform.Scale(width, height);

            // Attempt 3 - Using prepend as suggested in CGAffine Scale method comment
            // The Prepend flag needs to be used to get the same behaviour:
            affineTransform = CGAffineTransform.MakeTranslation(x, y);
            affineTransform.Scale(width, height, MatrixOrder.Prepend);

            return affineTransform;
        }
    }
}
