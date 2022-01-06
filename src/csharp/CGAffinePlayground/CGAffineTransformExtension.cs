using System;
using CoreGraphics;

namespace CGAffinePlayground
{
	public static class CGAffineTransformExtension
	{
		public static string ToPreciseString(this CGAffineTransform affineTransform)
        {
			//affineTransform = new CGAffineTransform(xx: 1341.0850423177085f, yx: 0.0f,
			//	xy: 0.0f, yy: 887.7420959472656f,
			//	x0: 1173.6544596354165f, y0: 1046.136474609375f);

			var xx = GetFloatOrZeroWithDecimal(affineTransform.xx);
			var yx = GetFloatOrZeroWithDecimal(affineTransform.yx);
			var xy = GetFloatOrZeroWithDecimal(affineTransform.xy);
			var yy = GetFloatOrZeroWithDecimal(affineTransform.yy);
			var x0 = GetFloatOrZeroWithDecimal(affineTransform.x0);
			var y0 = GetFloatOrZeroWithDecimal(affineTransform.y0);

			return $"xx:{xx} yx:{yx} xy:{xy} yy:{yy} x0:{x0} y0:{y0}";
        }

		internal static string GetFloatOrZeroWithDecimal(nfloat value)
        {
			return value != 0 ? value.ToString("#.######") : "0.0";
        }
	}
}

