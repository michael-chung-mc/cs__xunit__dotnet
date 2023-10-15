using LibTuple;
using LibComparinator;
namespace LibMatrix;

public class Matrix {
	public List<List<double>> _fieldGrid;
	public int _fieldRows;
	public int _fieldColumns;
	public Matrix()
	{
		_fieldRows = 4;
		_fieldColumns = 4;
		_fieldGrid = new List<List<double>>();
		for (int i = 0; i < _fieldRows; ++i)
		{
			_fieldGrid.Add(new List<double>());
			for (int j = 0; j < _fieldColumns; ++j)
			{
				_fieldGrid[i].Add(i == j ? 1 : 0);
			}
		}
	}
	public Matrix(int argRows, int argColumns)
	{
		_fieldRows = argRows;
		_fieldColumns = argColumns;
		_fieldGrid = new List<List<double>>();
		for (int i = 0; i < _fieldRows; ++i)
		{
			_fieldGrid.Add(new List<double>());
			for (int j = 0; j < _fieldColumns; ++j)
			{
				_fieldGrid[i].Add(i == j ? 1 : 0);
			}
		}
	}
	public Matrix(Matrix argOther)
	{
		_fieldRows = argOther._fieldRows;
		_fieldColumns = argOther._fieldColumns;
		_fieldGrid = new List<List<double>>();
		for (int i = 0; i < _fieldRows; ++i)
		{
			_fieldGrid.Add(new List<double>());
			for (int j = 0; j < _fieldColumns; ++j)
			{
				_fieldGrid[i].Add(argOther._fieldGrid[i][j]);
			}
		}
	}
	public Matrix(int argRows, int argColumns, List<double> argValues)
	{
		_fieldRows = argRows;
		_fieldColumns = argColumns;
		_fieldGrid = new List<List<double>>();
		for (int i = 0; i < _fieldRows; ++i)
		{
			_fieldGrid.Add(new List<double>());
			for (int j = 0; j < _fieldColumns; ++j)
			{
				_fieldGrid[i].Add(argValues[(i * argColumns) + j]);
			}
		}
	}
	// public static bool operator==(Matrix argSelf, Matrix argOther)
	// {
	// 	if (argOther._fieldRows != argSelf._fieldRows || argOther._fieldColumns != argSelf._fieldColumns)
	// 	{
	// 		return false;
	// 	}
	// 	for (int i = 0; i < argSelf._fieldRows; ++i)
	// 	{
	// 		for (int j = 0; j < argSelf._fieldColumns; ++j)
	// 		{
	// 			if (argOther._fieldGrid[i][j] != argSelf._fieldGrid[i][j])
	// 			{
	// 				return false;
	// 			}
	// 		}
	// 	}
	// 	return true;
	// }
	// public static bool operator!=(Matrix argSelf, Matrix other)
	// {
	// 	if (other._fieldRows != argSelf._fieldRows || other._fieldColumns != argSelf._fieldColumns)
	// 	{
	// 		return true;
	// 	}
	// 	for (int i = 0; i < argSelf._fieldRows; ++i)
	// 	{
	// 		for (int j = 0; j < argSelf._fieldColumns; ++j)
	// 		{
	// 			if (other._fieldGrid[i][j] != argSelf._fieldGrid[i][j])
	// 			{
	// 				return true;
	// 			}
	// 		}
	// 	}
	// 	return false;
	// }
	public static Matrix operator*(Matrix argSelf, Matrix argOther)
	{
		Matrix varResult = new Matrix(argOther._fieldRows, argOther._fieldColumns);
		for (int i = 0; i < varResult._fieldRows; ++i)
		{
			for (int j = 0; j < varResult._fieldColumns; ++j)
			{
				double value = 0;
				for (int k = 0; k < varResult._fieldColumns; k++)
				{
					value += argSelf._fieldGrid[i][k] * argOther._fieldGrid[k][j];
				}
				varResult.SetRC(i, j, value);
			}
		}
		return varResult;
	}
	public static SpaceTuple operator*(Matrix argSelf, SpaceTuple argOther)
	{
		if (argSelf._fieldRows != 4 && argSelf._fieldColumns != 4) return argOther;
		List<double> varPseudoMatrix = new List<double>{argOther._fieldX, argOther._fieldY, argOther._fieldZ, argOther._fieldW};
		List<double> varResults = new List<double>{ 0,0,0,0 };
		double res = 0;
		for (int tuple = 0; tuple < 4; tuple++)
		{
			res = 0;
			for (int col = 0; col < argSelf._fieldColumns; col++)
			{
				res += argSelf._fieldGrid[tuple][col] * varPseudoMatrix[col];
			}
			varResults[tuple] = res;
		}
		return new SpaceTuple(varResults[0],varResults[1],varResults[2],varResults[3]);
	}
	public static Point operator*(Matrix argSelf, Point argOther)
	{
		SpaceTuple varTuple = new SpaceTuple(argOther._fieldX, argOther._fieldY, argOther._fieldZ, argOther._fieldW);
		SpaceTuple varResult = argSelf * varTuple;
		return new Point(varResult._fieldX, varResult._fieldY, varResult._fieldZ);
	}
	public static Vector operator*(Matrix argSelf, Vector other)
	{
		SpaceTuple varTuple = new SpaceTuple(other._fieldX, other._fieldY, other._fieldZ, other._fieldW);
		SpaceTuple varResult = argSelf * varTuple;
		return new Vector(varResult._fieldX, varResult._fieldY, varResult._fieldZ);
	}
	public bool CheckInvertible() {
		return GetDeterminant() != 0;
	}
	public bool CheckValid(int argPositionRow, int argPositionColumn)
	{
		return argPositionRow >= 0 && argPositionRow < _fieldRows && argPositionColumn >= 0 && argPositionColumn < _fieldColumns;
	}
	public bool CheckEqual(Matrix argOther)
	{
		if (argOther._fieldRows != _fieldRows || argOther._fieldColumns != _fieldColumns)
		{
			return false;
		}
		Comparinator varComp = new Comparinator();
		for (int i = 0; i < _fieldRows; ++i)
		{
			for (int j = 0; j < _fieldColumns; ++j)
			{
				if (!varComp.CheckFloat(argOther._fieldGrid[i][j],_fieldGrid[i][j]))
				{
					return false;
				}
			}
		}
		return true;
	}
	public double GetRC(int argPositionRow, int argPositionColumn)
	{
		if (CheckValid(argPositionRow, argPositionColumn))
		{
			return _fieldGrid[argPositionRow][argPositionColumn];
		}
		return 0;
	}
	public void SetRC(int argPositionRow, int argPositionColumn, double argValue)
	{
		if (CheckValid(argPositionRow, argPositionColumn))
		{
			_fieldGrid[argPositionRow][argPositionColumn] = argValue;
		}
	}
	public double GetDeterminant()
	{
		double varDeterminant = 0;
		if (_fieldRows == 2 && _fieldColumns == 2) varDeterminant = _fieldGrid[0][0] * _fieldGrid[1][1] - _fieldGrid[0][1] * _fieldGrid[1][0];
		else
		{
			for (int i = 0; i < _fieldColumns; ++i)
			{
				varDeterminant += GetCofactor(0, i) * GetRC(0, i);
			};
		};
		return varDeterminant;
	}
	public double GetMinor(int argPositionRow, int argPositionColumn)
	{
		return GetSubMatrix(argPositionRow, argPositionColumn).GetDeterminant();
	}
	public double GetCofactor(int argPositionRow, int argPositionColumn)
	{
		return (argPositionRow + argPositionColumn) % 2 == 0 ? GetMinor(argPositionRow, argPositionColumn) : -1 * GetMinor(argPositionRow, argPositionColumn);
	}
	public virtual Matrix GetTranspose()
	{
		Matrix varTranspose = new Matrix(_fieldColumns, _fieldRows);
		for (int i = 0; i < varTranspose._fieldRows; ++i)
		{
			for (int j = 0; j < varTranspose._fieldColumns; ++j)
			{
				varTranspose.SetRC(i, j, GetRC(j, i));
			};
		};
		return varTranspose;
	}
	public virtual Matrix GetSubMatrix(int argRows, int argColumns)
	{
		Matrix varSubMatrix = new Matrix(_fieldRows - 1, _fieldColumns - 1);
		int varSubRow = 0;
		int varSubCol = 0;
		int i = 0;
		int j = 0;
		while (i < _fieldRows)
		{
			if (i == argRows) { i += 1; }
			j = 0;
			varSubCol = 0;
			while (j < _fieldColumns)
			{
				if (j == argColumns) { j += 1; }
				varSubMatrix.SetRC(varSubRow, varSubCol, GetRC(i, j));
				j += 1;
				varSubCol += 1;
			}
			i += 1;
			varSubRow += 1;
		}
		return varSubMatrix;
	}
	public virtual Matrix GetInverse()
	{
		Matrix varInverse = new Matrix(_fieldRows, _fieldColumns);
		if (!CheckInvertible()) return varInverse;
		double dm = GetDeterminant();
		for (int i = 0; i < _fieldRows; ++i)
		{
			for (int j = 0; j < _fieldColumns; ++j)
			{
				double cf = GetCofactor(i, j);
				varInverse.SetRC(j, i, cf / dm);
			};
		};
		return varInverse;
	}
	public virtual Matrix GetIdentity()
	{
		Matrix varIdentity = new Matrix(_fieldRows, _fieldColumns);
		for (int i = 0; i < varIdentity._fieldRows; ++i)
		{
			for (int j = 0; j < varIdentity._fieldColumns; ++j)
			{
				varIdentity._fieldGrid[i][j] = (i == j ? 1 : 0);
			};
		};
		return varIdentity;
	}
	public virtual Matrix GetRotateX(double argRadians)
	{
		XRotationMatrix rotate = new XRotationMatrix(argRadians);
		return this * rotate;
	}
	public virtual Matrix GetRotateY(double argRadians)
	{
		YRotationMatrix rotate = new YRotationMatrix(argRadians);
		return this * rotate;
	}
	public virtual Matrix GetRotateZ(double argRadians)
	{
		ZRotationMatrix rotate = new ZRotationMatrix(argRadians);
		return this * rotate;
	}
	public virtual Matrix GetScale(double x, double y, double z)
	{
		ScalingMatrix scale = new ScalingMatrix(x,y,z);
		return this * scale;
	}
	public virtual Matrix GetTranslate(double x, double y, double z)
	{
		TranslationMatrix translate = new TranslationMatrix(x,y,z);
		return this * translate;
	}
	public virtual Matrix GetShear(double xy, double xz, double yx, double yz, double zx, double zy)
	{
		ShearingMatrix shear = new ShearingMatrix(xy, xz, yx, yz, zx, zy);
		return this * shear;
	}
	public virtual void RenderConsole() 
	{
		Console.WriteLine("Matrix::renderConsole()");
		for (int i = 0; i < _fieldRows; ++i)
		{
			Console.Write("[");
			for (int j = 0; j < _fieldColumns; ++j)
			{
				Console.Write($"{_fieldGrid[i][j]}, ");
			};
			Console.WriteLine("]");
		};
	}
};
public class IdentityMatrix : Matrix {
	public IdentityMatrix(int rows, int columns) {
		for (int i = 0; i < _fieldRows; ++i)
		{
			for (int j = 0; j < _fieldColumns; ++j)
			{
				_fieldGrid[i][j] = (i == j ?  1 : 0);
			};
		};
	}
};
public class TranslationMatrix : Matrix {
	public TranslationMatrix(double x, double y, double z) {
		_fieldGrid[0][0] = 1;
		_fieldGrid[0][3] = x;
		_fieldGrid[1][3] = y;
		_fieldGrid[1][1] = 1;
		_fieldGrid[2][3] = z;
		_fieldGrid[2][2] = 1;
		_fieldGrid[3][3] = 1;
	}

};
public class ScalingMatrix : Matrix {
	public ScalingMatrix(double x, double y, double z) {
		_fieldGrid[0][0] = x;
		_fieldGrid[1][1] = y;
		_fieldGrid[2][2] = z;
		_fieldGrid[3][3] = 1;
	}
};
public class XRotationMatrix : Matrix {
	public XRotationMatrix(double radians) {
		_fieldGrid[0][0] = 1;
		_fieldGrid[1][1] = Math.Cos(radians);
		_fieldGrid[1][2] = -Math.Sin(radians);
		_fieldGrid[2][1] = Math.Sin(radians);
		_fieldGrid[2][2] = Math.Cos(radians);
		_fieldGrid[3][3] = 1;
	}
};
public class YRotationMatrix : Matrix {
	public YRotationMatrix(double radians) {
		_fieldGrid[0][0] = Math.Cos(radians);
		_fieldGrid[0][2] = Math.Sin(radians);
		_fieldGrid[1][1] = 1;
		_fieldGrid[2][0] = -Math.Sin(radians);
		_fieldGrid[2][2] = Math.Cos(radians);
		_fieldGrid[3][3] = 1;
	}
};
public class ZRotationMatrix : Matrix {
	public ZRotationMatrix(double radians) {
		_fieldGrid[0][0] = Math.Cos(radians);
		_fieldGrid[0][1] = -Math.Sin(radians);
		_fieldGrid[1][0] = Math.Sin(radians);
		_fieldGrid[1][1] = Math.Cos(radians);
		_fieldGrid[2][2] = 1;
		_fieldGrid[3][3] = 1;
	}
};
public class ShearingMatrix : Matrix {
	public ShearingMatrix(double xy, double xz, double yx, double yz, double zx, double zy) {
		_fieldGrid[0][0] = 1;
		_fieldGrid[0][1] = xy;
		_fieldGrid[0][2] = xz;
		_fieldGrid[1][0] = yx;
		_fieldGrid[1][1] = 1;
		_fieldGrid[1][2] = yz;
		_fieldGrid[2][0] = zx;
		_fieldGrid[2][1] = zy;
		_fieldGrid[2][2] = 1;
		_fieldGrid[3][3] = 1;
	}
};
public class ViewMatrix : Matrix {
	public ViewMatrix()
	{
		for (int i = 0; i < _fieldRows; ++i)
		{
			for (int j = 0; j < _fieldColumns; ++j)
			{
				_fieldGrid[i][j] = (i == j ?  1 : 0);
			};
		};
	}
	public ViewMatrix(Point argStart, Point argEnd, Vector argUp) {
		Vector varForward = (argEnd - argStart).GetNormal();
		Vector varLeft = varForward.GetCrossProduct(argUp.GetNormal());
		Vector varUp = varLeft.GetCrossProduct(varForward);
		List<double> varValues = new List<double>{varLeft._fieldX, varLeft._fieldY, varLeft._fieldZ, 0, varUp._fieldX, varUp._fieldY, varUp._fieldZ, 0, -varForward._fieldX, -varForward._fieldY, -varForward._fieldZ, 0, 0, 0, 0, 1};
		Matrix varOrientation = new Matrix(4,4, varValues);
		Matrix varResult = varOrientation * new TranslationMatrix(-argStart._fieldX, -argStart._fieldY, -argStart._fieldZ);
		for (int i = 0; i < _fieldRows; ++i)
		{
			for (int j = 0; j < _fieldColumns; ++j)
			{
				_fieldGrid[i][j] = varResult._fieldGrid[i][j];
			};
		};
	}
};