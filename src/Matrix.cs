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
		//std::cout << "matrix copy ctor" << std::endl;
		_fieldRows = argOther._fieldRows;
		_fieldColumns = argOther._fieldColumns;
		//std::cout << "this: " << mbrRows << mbrColumns << " other:" << other.mbrRows << other.mbrColumns << std::endl;
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
		// mbrGrid = new double* [mbrRows];
		// for (int i = 0; i < mbrRows; ++i)
		// {
		// 	mbrGrid[i] = new double[mbrColumns];
		// }
		// for (int i = 0; i < mbrRows; ++i)
		// {
		// 	for (int j = 0; j < mbrColumns; ++j)
		// 	{
		// 		mbrGrid[i][j] = values[(i * columns) + j];
		// 	}
		// }
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
	// public Matrix operator=(Matrix other)
	// {
	// 	if (this == &other) return *this;
	// 	//std::cout << "matrix copy assign" << std::endl;
	// 	mbrRows = other.mbrRows;
	// 	mbrColumns = other.mbrColumns;
	// 	for (int i = 0; i < mbrRows; ++i)
	// 	{
	// 		mbrGrid.Add(List<double>());
	// 		for (int j = 0; j < mbrColumns; ++j)
	// 		{
	// 			mbrGrid[i][j] = other.mbrGrid[i][j];
	// 		}
	// 	}
		// for (int i = 0; i < mbrRows; ++i)
		// {
		// 	delete[] mbrGrid[i];
		// 	mbrGrid[i] = nullptr;
		// }
		// delete[] mbrGrid;
		// mbrGrid = nullptr;
		// mbrGrid = new double* [mbrRows];
		// for (int i = 0; i < mbrRows; ++i)
		// {
		// 	mbrGrid[i] = new double[mbrColumns];
		// }
		// for (int i = 0; i < mbrRows; ++i)
		// {
		// 	for (int j = 0; j < mbrColumns; ++j)
		// 	{
		// 		mbrGrid[i][j] = other.mbrGrid[i][j];
		// 	}
		// }
	// 	return *this;
	// }
	public static bool operator==(Matrix argSelf, Matrix argOther)
	{
		//std::cout << "matrix equality operator" << std::endl;
		if (argOther._fieldRows != argSelf._fieldRows || argOther._fieldColumns != argSelf._fieldColumns)
		{
			return false;
		}
		for (int i = 0; i < argSelf._fieldRows; ++i)
		{
			for (int j = 0; j < argSelf._fieldColumns; ++j)
			{
				//std::cout << "addresses this:" << &grid[i][j] << ":other:" << &other.grid[i][j] << std::endl;
				//std::cout << "values this:" << grid[i][j] << ":other:" << other.grid[i][j] << std::endl;
				if (argOther._fieldGrid[i][j] != argSelf._fieldGrid[i][j])
				{
					return false;
				}
			}
		}
		return true;
	}
	public static bool operator!=(Matrix argSelf, Matrix other)
	{
		//std::cout << "matrix equality operator" << std::endl;
		if (other._fieldRows != argSelf._fieldRows || other._fieldColumns != argSelf._fieldColumns)
		{
			return true;
		}
		for (int i = 0; i < argSelf._fieldRows; ++i)
		{
			for (int j = 0; j < argSelf._fieldColumns; ++j)
			{
				//std::cout << "addresses this:" << &grid[i][j] << ":other:" << &other.grid[i][j] << std::endl;
				//std::cout << "values this:" << grid[i][j] << ":other:" << other.grid[i][j] << std::endl;
				if (other._fieldGrid[i][j] != argSelf._fieldGrid[i][j])
				{
					return true;
				}
			}
		}
		return false;
	}
	public static Matrix operator*(Matrix argSelf, Matrix argOther)
	{
		//std::cout << "matrix multiplication" << std::endl;
		Matrix varResult = new Matrix(argOther._fieldRows, argOther._fieldColumns);
		for (int i = 0; i < varResult._fieldRows; ++i)
		{
			for (int j = 0; j < varResult._fieldColumns; ++j)
			{
				double value = 0;
				for (int k = 0; k < varResult._fieldColumns; k++)
				{
					//std::cout << "i:" << i << "j:" << j << "k:" << k << "=" << grid[i][k] << "*" << other.grid[k][j] << std::endl;
					value += argSelf._fieldGrid[i][k] * argOther._fieldGrid[k][j];
				}
				//std::cout << value << std::endl;
				varResult.SetRC(i, j, value);
			}
		}
		return varResult;
	}
	public static SpaceTuple operator*(Matrix argSelf, SpaceTuple argOther)
	{
		if (argSelf._fieldRows != 4 && argSelf._fieldColumns != 4) return argOther;
		//std::cout << "matrix tuple multiplication" << std::endl;
		List<double> varPseudoMatrix = new List<double>{argOther._fieldX, argOther._fieldY, argOther._fieldZ, argOther._fieldW};
		List<double> varResults = new List<double>{ 0,0,0,0 };
		double res = 0;
		for (int tuple = 0; tuple < 4; tuple++)
		{
			res = 0;
			for (int col = 0; col < argSelf._fieldColumns; col++)
			{
				//std::cout << "i:" << col << "j:" << tuple << "k:" << res << "=" << pseudoMatrix[col] << "*" << grid[tuple][col] << std::endl;
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
		//std::cout << "matrix equality" << std::endl;
		if (argOther._fieldRows != _fieldRows || argOther._fieldColumns != _fieldColumns)
		{
			return false;
		}
		Comparinator varComp = new Comparinator();
		//std::cout << "row == column" << std::endl;
		for (int i = 0; i < _fieldRows; ++i)
		{
			for (int j = 0; j < _fieldColumns; ++j)
			{
				//std::cout << "addresses this:" << & grid[i][j] << ":other:" << &other.grid[i][j] << std::endl;
				//std::cout << "values this:" << grid[i][j] << ":other:" << other.grid[i][j] << std::endl;
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
	public Matrix GetTranspose()
	{
		Matrix varTranspose = new Matrix(_fieldColumns, _fieldRows);
		for (int i = 0; i < varTranspose._fieldRows; ++i)
		{
			for (int j = 0; j < varTranspose._fieldColumns; ++j)
			{
				varTranspose.SetRC(i, j, GetRC(j, i));
				//std::cout << "i:" << i << "j:" << j << "now: " << getRC(j, i) << "=" << copy->getRC(i, j) << std::endl;
			};
		};
		return varTranspose;
	}
	public Matrix GetSubMatrix(int argRows, int argColumns)
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
				//std::cout << "i:" << subrow << "j:" << subcol << "now: " << getRC(i, j) << "=" << sub->getRC(subrow, subcol) << std::endl;
				j += 1;
				varSubCol += 1;
			}
			i += 1;
			varSubRow += 1;
		}
		return varSubMatrix;
	}
	public Matrix GetInverse()
	{
		// std::cout << "Matrix::invert()" << std::endl;
		Matrix varInverse = new Matrix(_fieldRows, _fieldColumns);
		if (!CheckInvertible()) return varInverse;
		double dm = GetDeterminant();
		for (int i = 0; i < _fieldRows; ++i)
		{
			for (int j = 0; j < _fieldColumns; ++j)
			{
				double cf = GetCofactor(i, j);
				varInverse.SetRC(j, i, cf / dm);
				// std::cout << "j:" << j << "i:" << i << "now: " << cf << "/" << dm << "=" << inverse->getRC(j, i) << std::endl;
			};
		};
		return varInverse;
	}
	public Matrix Identity()
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
	public Matrix GetRotateX(double argRadians)
	{
		XRotationMatrix rotate = new XRotationMatrix(argRadians);
		return this * rotate;
	}
	public Matrix GetRotateY(double argRadians)
	{
		YRotationMatrix rotate = new YRotationMatrix(argRadians);
		return this * rotate;
	}
	public Matrix GetRotateZ(double argRadians)
	{
		ZRotationMatrix rotate = new ZRotationMatrix(argRadians);
		return this * rotate;
	}
	public Matrix GetScale(double x, double y, double z)
	{
		ScalingMatrix scale = new ScalingMatrix(x,y,z);
		return this * scale;
	}
	public Matrix getTranslate(double x, double y, double z)
	{
		TranslationMatrix translate = new TranslationMatrix(x,y,z);
		return this * translate;
	}
	public Matrix GetShear(double xy, double xz, double yx, double yz, double zx, double zy)
	{
		ShearingMatrix shear = new ShearingMatrix(xy, xz, yx, yz, zx, zy);
		return this * shear;
	}
	public void RenderConsole() 
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
				// std::cout << "addresses this:" << & grid[i][j] << ":other:" << &varResult->grid[i][j] << std::endl;
				// std::cout << "values this:" << grid[i][j] << ":other:" << varResult->grid[i][j] << std::endl;
			};
		};
	}
};