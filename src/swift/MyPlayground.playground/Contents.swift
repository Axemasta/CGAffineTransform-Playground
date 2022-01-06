import UIKit
import CoreGraphics
import Vision

let displaySize = CGSize(width: 4032, height: 3024)
let faceObservationBoundingBox = CGRect(x: 0.291085, y: 0.345945, width: 0.3326104, height: 0.293566)

let faceBounds = VNImageRectForNormalizedRect(faceObservationBoundingBox, Int(displaySize.width), Int(displaySize.height))

let affineTransform = CGAffineTransform(translationX: faceBounds.origin.x, y: faceBounds.origin.y)
    .scaledBy(x: faceBounds.size.width, y: faceBounds.size.height)

// This should print the following transformation:
// a = 1341.0851328
// b = 0.0
// c = 0.0
// d = 887.743584
// tx = 1173.65472
// ty = 1046.13768
print(affineTransform)
