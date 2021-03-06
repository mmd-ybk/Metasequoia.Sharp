﻿using System;
namespace Metasequoia
{
	public struct Matrix
	{
		public float M11;
		public float M12;
		public float M13;
		public float M14;
		public float M21;
		public float M22;
		public float M23;
		public float M24;
		public float M31;
		public float M32;
		public float M33;
		public float M34;
		public float M41;
		public float M42;
		public float M43;
		public float M44;

		public static readonly Matrix Identity = new Matrix(1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);

		public Matrix(float[] t)
		{
			M11 = t[0];
			M12 = t[1];
			M13 = t[2];
			M14 = t[3];
			M21 = t[4];
			M22 = t[5];
			M23 = t[6];
			M24 = t[7];
			M31 = t[8];
			M32 = t[9];
			M33 = t[10];
			M34 = t[11];
			M41 = t[12];
			M42 = t[13];
			M43 = t[14];
			M44 = t[15];
		}

		public Matrix(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
		{
			this.M11 = m11;
			this.M12 = m12;
			this.M13 = m13;
			this.M14 = m14;
			this.M21 = m21;
			this.M22 = m22;
			this.M23 = m23;
			this.M24 = m24;
			this.M31 = m31;
			this.M32 = m32;
			this.M33 = m33;
			this.M34 = m34;
			this.M41 = m41;
			this.M42 = m42;
			this.M43 = m43;
			this.M44 = m44;
		}

		public float[] ToArray()
		{
			return new[]
			{
				M11, M12, M13, M14,
				M21, M22, M23, M24,
				M31, M32, M33, M34,
				M41, M42, M43, M44,
			};
		}

		public static Matrix CreateFromYawPitchRoll(float yaw, float pitch, float roll)
		{
			return CreateFromYawPitchRoll(new Angle(yaw, pitch, roll));
		}

		public static Matrix CreateFromYawPitchRoll(Angle angle)
		{
			return Matrix.Identity.SetTransform(Point.One, angle, Point.Zero);
		}

		public static Matrix CreateFromAxisAngle(Point axis, float radians)
		{
			var x = axis.X;
			var y = axis.Y;
			var z = axis.Z;
			var num = (float)Math.Sin(radians);
			var num2 = (float)Math.Cos(radians);
			var num3 = x * x;
			var num4 = y * y;
			var num5 = z * z;
			var num6 = x * y;
			var num7 = x * z;
			var num8 = y * z;
			var result = new Matrix();

			result.M11 = num3 + num2 * (1f - num3);
			result.M12 = num6 - num2 * num6 + num * z;
			result.M13 = num7 - num2 * num7 - num * y;
			result.M14 = 0f;
			result.M21 = num6 - num2 * num6 - num * z;
			result.M22 = num4 + num2 * (1f - num4);
			result.M23 = num8 - num2 * num8 + num * x;
			result.M24 = 0f;
			result.M31 = num7 - num2 * num7 + num * y;
			result.M32 = num8 - num2 * num8 - num * x;
			result.M33 = num5 + num2 * (1f - num5);
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;

			return result;
		}

		public static Matrix CreateTranslation(Point p)
		{
			return Matrix.Identity.SetTransform(Point.One, Angle.Zero, p);
		}

		public static Matrix CreateScale(Point s)
		{
			return Matrix.Identity.SetTransform(s, Angle.Zero, Point.Zero);
		}

		public static Matrix Invert(Matrix matrix)
		{
			var result = new Matrix();
			var m = matrix.M11;
			var m2 = matrix.M12;
			var m3 = matrix.M13;
			var m4 = matrix.M14;
			var m5 = matrix.M21;
			var m6 = matrix.M22;
			var m7 = matrix.M23;
			var m8 = matrix.M24;
			var m9 = matrix.M31;
			var m10 = matrix.M32;
			var m11 = matrix.M33;
			var m12 = matrix.M34;
			var m13 = matrix.M41;
			var m14 = matrix.M42;
			var m15 = matrix.M43;
			var m16 = matrix.M44;
			var num = m11 * m16 - m12 * m15;
			var num2 = m10 * m16 - m12 * m14;
			var num3 = m10 * m15 - m11 * m14;
			var num4 = m9 * m16 - m12 * m13;
			var num5 = m9 * m15 - m11 * m13;
			var num6 = m9 * m14 - m10 * m13;
			var num7 = m6 * num - m7 * num2 + m8 * num3;
			var num8 = -(m5 * num - m7 * num4 + m8 * num5);
			var num9 = m5 * num2 - m6 * num4 + m8 * num6;
			var num10 = -(m5 * num3 - m6 * num5 + m7 * num6);
			var num11 = 1f / (m * num7 + m2 * num8 + m3 * num9 + m4 * num10);
			var num12 = m7 * m16 - m8 * m15;
			var num13 = m6 * m16 - m8 * m14;
			var num14 = m6 * m15 - m7 * m14;
			var num15 = m5 * m16 - m8 * m13;
			var num16 = m5 * m15 - m7 * m13;
			var num17 = m5 * m14 - m6 * m13;
			var num18 = m7 * m12 - m8 * m11;
			var num19 = m6 * m12 - m8 * m10;
			var num20 = m6 * m11 - m7 * m10;
			var num21 = m5 * m12 - m8 * m9;
			var num22 = m5 * m11 - m7 * m9;
			var num23 = m5 * m10 - m6 * m9;

			result.M11 = num7 * num11;
			result.M21 = num8 * num11;
			result.M31 = num9 * num11;
			result.M41 = num10 * num11;
			result.M12 = -(m2 * num - m3 * num2 + m4 * num3) * num11;
			result.M22 = (m * num - m3 * num4 + m4 * num5) * num11;
			result.M32 = -(m * num2 - m2 * num4 + m4 * num6) * num11;
			result.M42 = (m * num3 - m2 * num5 + m3 * num6) * num11;
			result.M13 = (m2 * num12 - m3 * num13 + m4 * num14) * num11;
			result.M23 = -(m * num12 - m3 * num15 + m4 * num16) * num11;
			result.M33 = (m * num13 - m2 * num15 + m4 * num17) * num11;
			result.M43 = -(m * num14 - m2 * num16 + m3 * num17) * num11;
			result.M14 = -(m2 * num18 - m3 * num19 + m4 * num20) * num11;
			result.M24 = (m * num18 - m3 * num21 + m4 * num22) * num11;
			result.M34 = -(m * num19 - m2 * num21 + m4 * num23) * num11;
			result.M44 = (m * num20 - m2 * num22 + m3 * num23) * num11;

			return result;
		}

		/// <summary>
		/// 行列のうち左上 3x3 成分のみでベクトルと行列の積を計算します。
		/// const MQPoint MQMatrix::Mult3(const MQPoint&amp; p);
		/// </summary>
		/// <param name="p">ベクトル</param>
		/// <returns>ベクトルと行列の積</returns>
		public Point Mult3(Point p)
		{
			return new Point
			(
				p.X * M11 + p.Y * M21 + p.Z * M31,
				p.X * M12 + p.Y * M22 + p.Z * M32,
				p.X * M13 + p.Y * M23 + p.Z * M33
			);
		}

		/// <summary>
		/// 行列のうち左上 3x3 成分のみを転置します。
		/// void MQMatrix::Transpose3(void);
		/// </summary>
		/// <returns>転置行列</returns>
		public Matrix Transpose3()
		{
			return new Matrix
			(
				M11, M21, M31, M14,
				M12, M22, M32, M24,
				M13, M23, M33, M34,
				M41, M42, M43, M44
			);
		}

		/// <summary>
		/// SRT 変換行列から拡大成分を抽出して、その XYZ ごとの要素を MQPoint 型として取得します。
		/// MQPoint MQMatrix::GetScaling(void) const;
		/// </summary>
		/// <returns>拡大成分</returns>
		public Point GetScaling()
		{
			var val = new float[3];

			NativeMethods.MQMatrix_FloatValue(this.ToArray(), (int)MQMatrix.GetScaling, val);

			return new Point(val[0], val[1], val[2]);
		}

		/// <summary>
		/// SRT 変換行列から回転成分を抽出して、その角度（オイラー角）を MQAngle 型として取得します。
		/// MQAngle MQMatrix::GetRotation(void) const;
		/// </summary>
		/// <returns>回転成分</returns>
		public Angle GetRotation()
		{
			var val = new float[3];

			NativeMethods.MQMatrix_FloatValue(this.ToArray(), (int)MQMatrix.GetRotation, val);

			return new Angle(val[0], val[1], val[2]);
		}

		/// <summary>
		/// SRT 変換行列から平行移動成分を抽出して、その移動量を MQPoint 型として取得します。
		/// MQPoint MQMatrix::GetTranslation(void) const;
		/// </summary>
		/// <returns>平行移動成分</returns>
		public Point GetTranslation()
		{
			var val = new float[3];

			NativeMethods.MQMatrix_FloatValue(this.ToArray(), (int)MQMatrix.GetTranslation, val);

			return new Point(val[0], val[1], val[2]);
		}

		/// <summary>
		/// SRT 変換行列を設定します。
		/// void MQMatrix::SetTransform(const MQPoint *scaling, const MQAngle *rotation, const MQPoint *trans);
		/// </summary>
		/// <param name="scaling">拡大成分</param>
		/// <param name="rotation">回転成分</param>
		/// <param name="trans">平行移動成分</param>
		/// <returns>設定された行列</returns>
		public Matrix SetTransform(Point scaling, Angle rotation, Point trans)
		{
			var val = new float[]
			{
				scaling.X, scaling.Y, scaling.Z,
				rotation.Head, rotation.Pitch, rotation.Bank,
				trans.X, trans.Y, trans.Z
			};
			var rt = this.ToArray();

			NativeMethods.MQMatrix_FloatValue(rt, (int)MQMatrix.SetTransform, val);

			return new Matrix(rt);
		}

		/// <summary>
		/// SRT 変換逆行列を設定します。
		/// void MQMatrix::SetInverseTransform(const MQPoint *scaling, const MQAngle *rotation, const MQPoint *trans);
		/// </summary>
		/// <param name="scaling">拡大成分</param>
		/// <param name="rotation">回転成分</param>
		/// <param name="trans">平行移動成分</param>
		/// <returns>設定された逆行列</returns>
		public Matrix SetInverseTransform(Point scaling, Angle rotation, Point trans)
		{
			var val = new float[]
			{
				scaling.X, scaling.Y, scaling.Z,
				rotation.Head, rotation.Pitch, rotation.Bank,
				trans.X, trans.Y, trans.Z
			};
			var rt = this.ToArray();

			NativeMethods.MQMatrix_FloatValue(rt, (int)MQMatrix.SetInverseTransform, val);

			return new Matrix(rt);
		}

		public static Matrix operator +(Matrix a, Matrix b)
		{
			var result = new Matrix();

			result.M11 = a.M11 + b.M11;
			result.M12 = a.M12 + b.M12;
			result.M13 = a.M13 + b.M13;
			result.M14 = a.M14 + b.M14;
			result.M21 = a.M21 + b.M21;
			result.M22 = a.M22 + b.M22;
			result.M23 = a.M23 + b.M23;
			result.M24 = a.M24 + b.M24;
			result.M31 = a.M31 + b.M31;
			result.M32 = a.M32 + b.M32;
			result.M33 = a.M33 + b.M33;
			result.M34 = a.M34 + b.M34;
			result.M41 = a.M41 + b.M41;
			result.M42 = a.M42 + b.M42;
			result.M43 = a.M43 + b.M43;
			result.M44 = a.M44 + b.M44;

			return result;
		}

		public static Matrix operator -(Matrix a, Matrix b)
		{
			var result = new Matrix();

			result.M11 = a.M11 - b.M11;
			result.M12 = a.M12 - b.M12;
			result.M13 = a.M13 - b.M13;
			result.M14 = a.M14 - b.M14;
			result.M21 = a.M21 - b.M21;
			result.M22 = a.M22 - b.M22;
			result.M23 = a.M23 - b.M23;
			result.M24 = a.M24 - b.M24;
			result.M31 = a.M31 - b.M31;
			result.M32 = a.M32 - b.M32;
			result.M33 = a.M33 - b.M33;
			result.M34 = a.M34 - b.M34;
			result.M41 = a.M41 - b.M41;
			result.M42 = a.M42 - b.M42;
			result.M43 = a.M43 - b.M43;
			result.M44 = a.M44 - b.M44;

			return result;
		}

		public static Matrix operator *(Matrix a, Matrix b)
		{
			return new Matrix
			{
				M11 = a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31 + a.M14 * b.M41,
				M12 = a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32 + a.M14 * b.M42,
				M13 = a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33 + a.M14 * b.M43,
				M14 = a.M11 * b.M14 + a.M12 * b.M24 + a.M13 * b.M34 + a.M14 * b.M44,
				M21 = a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31 + a.M24 * b.M41,
				M22 = a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32 + a.M24 * b.M42,
				M23 = a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33 + a.M24 * b.M43,
				M24 = a.M21 * b.M14 + a.M22 * b.M24 + a.M23 * b.M34 + a.M24 * b.M44,
				M31 = a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31 + a.M34 * b.M41,
				M32 = a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32 + a.M34 * b.M42,
				M33 = a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33 + a.M34 * b.M43,
				M34 = a.M31 * b.M14 + a.M32 * b.M24 + a.M33 * b.M34 + a.M34 * b.M44,
				M41 = a.M41 * b.M11 + a.M42 * b.M21 + a.M43 * b.M31 + a.M44 * b.M41,
				M42 = a.M41 * b.M12 + a.M42 * b.M22 + a.M43 * b.M32 + a.M44 * b.M42,
				M43 = a.M41 * b.M13 + a.M42 * b.M23 + a.M43 * b.M33 + a.M44 * b.M43,
				M44 = a.M41 * b.M14 + a.M42 * b.M24 + a.M43 * b.M34 + a.M44 * b.M44
			};
		}
	}
}
