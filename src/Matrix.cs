using LibTuple;
using LibComparinator;
namespace LibMatrix;

public class Matrix {
	public List<List<double>> mbrGrid;
	public int mbrRows;
	public int mbrColumns;
	public Matrix()
	{
		mbrRows = 4;
		mbrColumns = 4;
		mbrGrid = new List<List<double>>();
		for (int i = 0; i < mbrRows; ++i)
		{
			mbrGrid.Add(new List<double>());
			for (int j = 0; j < mbrColumns; ++j)
			{
				mbrGrid[i].Add(i == j ? 1 : 0);
			}
		}
	}
	public Matrix(int argRows, int argColumns)
	{
		mbrRows = argRows;
		mbrColumns = argColumns;
		mbrGrid = new List<List<double>>();
		for (int i = 0; i < mbrRows; ++i)
		{
			mbrGrid.Add(new List<double>());
			for (int j = 0; j < mbrColumns; ++j)
			{
				mbrGrid[i].Add(i == j ? 1 : 0);
			}
		}
	}
	public Matrix(Matrix argOther)
	{
		//std::cout << "matrix copy ctor" << std::endl;
		mbrRows = argOther.mbrRows;
		mbrColumns = argOther.mbrColumns;
		//std::cout << "this: " << mbrRows << mbrColumns << " other:" << other.mbrRows << other.mbrColumns << std::endl;
		mbrGrid = new List<List<double>>();
		for (int i = 0; i < mbrRows; ++i)
		{
			mbrGrid.Add(new List<double>());
			for (int j = 0; j < mbrColumns; ++j)
			{
				mbrGrid[i].Add(argOther.mbrGrid[i][j]);
			}
		}
	}
	public Matrix(int rows, int columns, List<double> values)
	{
		mbrRows = rows;
		mbrColumns = columns;
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
		mbrGrid = new List<List<double>>();
		for (int i = 0; i < mbrRows; ++i)
		{
			mbrGrid.Add(new List<double>());
			for (int j = 0; j < mbrColumns; ++j)
			{
				mbrGrid[i].Add(values[(i * columns) + j]);
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
	public static bool operator==(Matrix argSelf, Matrix other)
	{
		//std::cout << "matrix equality operator" << std::endl;
		if (other.mbrRows != argSelf.mbrRows || other.mbrColumns != argSelf.mbrColumns)
		{
			return false;
		}
		for (int i = 0; i < argSelf.mbrRows; ++i)
		{
			for (int j = 0; j < argSelf.mbrColumns; ++j)
			{
				//std::cout << "addresses this:" << &grid[i][j] << ":other:" << &other.grid[i][j] << std::endl;
				//std::cout << "values this:" << grid[i][j] << ":other:" << other.grid[i][j] << std::endl;
				if (other.mbrGrid[i][j] != argSelf.mbrGrid[i][j])
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
		if (other.mbrRows != argSelf.mbrRows || other.mbrColumns != argSelf.mbrColumns)
		{
			return true;
		}
		for (int i = 0; i < argSelf.mbrRows; ++i)
		{
			for (int j = 0; j < argSelf.mbrColumns; ++j)
			{
				//std::cout << "addresses this:" << &grid[i][j] << ":other:" << &other.grid[i][j] << std::endl;
				//std::cout << "values this:" << grid[i][j] << ":other:" << other.grid[i][j] << std::endl;
				if (other.mbrGrid[i][j] != argSelf.mbrGrid[i][j])
				{
					return true;
				}
			}
		}
		return false;
	}
	public static Matrix operator*(Matrix self, Matrix other)
	{
		//std::cout << "matrix multiplication" << std::endl;
		Matrix result = new Matrix(other.mbrRows, other.mbrColumns);
		for (int i = 0; i < result.mbrRows; ++i)
		{
			for (int j = 0; j < result.mbrColumns; ++j)
			{
				double value = 0;
				for (int k = 0; k < result.mbrColumns; k++)
				{
					//std::cout << "i:" << i << "j:" << j << "k:" << k << "=" << grid[i][k] << "*" << other.grid[k][j] << std::endl;
					value += self.mbrGrid[i][k] * other.mbrGrid[k][j];
				}
				//std::cout << value << std::endl;
				result.setRC(i, j, value);
			}
		}
		return result;
	}
	public static SpaceTuple operator*(Matrix self, SpaceTuple other)
	{
		if (self.mbrRows != 4 && self.mbrColumns != 4) return other;
		//std::cout << "matrix tuple multiplication" << std::endl;
		List<double> pseudoMatrix = new List<double>{other._fieldX, other._fieldY, other._fieldZ, other._fieldW};
		List<double> results = new List<double>{ 0,0,0,0 };
		double res = 0;
		for (int tuple = 0; tuple < 4; tuple++)
		{
			res = 0;
			for (int col = 0; col < self.mbrColumns; col++)
			{
				//std::cout << "i:" << col << "j:" << tuple << "k:" << res << "=" << pseudoMatrix[col] << "*" << grid[tuple][col] << std::endl;
				res += self.mbrGrid[tuple][col] * pseudoMatrix[col];
			}
			results[tuple] = res;
		}
		return new SpaceTuple(results[0],results[1],results[2],results[3]);
	}
	public static Point operator*(Matrix argSelf, Point other)
	{
		SpaceTuple tp = new SpaceTuple(other._fieldX, other._fieldY, other._fieldZ, other._fieldW);
		SpaceTuple res = argSelf * tp;
		return new Point(res._fieldX, res._fieldY, res._fieldZ);
	}
	public static Vector operator*(Matrix argSelf, Vector other)
	{
		SpaceTuple tp = new SpaceTuple(other._fieldX, other._fieldY, other._fieldZ, other._fieldW);
		SpaceTuple res = argSelf * tp;
		return new Vector(res._fieldX, res._fieldY, res._fieldZ);
	}
	public bool CheckInvertible() {
		return determinant() != 0;
	}
	public bool CheckValid(int row, int column)
	{
		return row >= 0 && row < mbrRows && column >= 0 && column < mbrColumns;
	}
	public bool CheckEqual(Matrix other)
	{
		//std::cout << "matrix equality" << std::endl;
		if (other.mbrRows != mbrRows || other.mbrColumns != mbrColumns)
		{
			return false;
		}
		Comparinator ce = new Comparinator();
		//std::cout << "row == column" << std::endl;
		for (int i = 0; i < mbrRows; ++i)
		{
			for (int j = 0; j < mbrColumns; ++j)
			{
				//std::cout << "addresses this:" << & grid[i][j] << ":other:" << &other.grid[i][j] << std::endl;
				//std::cout << "values this:" << grid[i][j] << ":other:" << other.grid[i][j] << std::endl;
				if (!ce.CheckFloat(other.mbrGrid[i][j],mbrGrid[i][j]))
				{
					return false;
				}
			}
		}
		return true;
	}
	public double getRC(int row, int column)
	{
		if (CheckValid(row, column))
		{
			return mbrGrid[row][column];
		}
		return 0;
	}
	public void setRC(int row, int column, double value)
	{
		if (CheckValid(row, column))
		{
			mbrGrid[row][column] = value;
		}
	}
	public double determinant()
	{
		double determinant = 0;
		if (mbrRows == 2 && mbrColumns == 2) determinant = mbrGrid[0][0] * mbrGrid[1][1] - mbrGrid[0][1] * mbrGrid[1][0];
		else
		{
			for (int i = 0; i < mbrColumns; ++i)
			{
				determinant += cofactor(0, i) * getRC(0, i);
			};
		};
		return determinant;
	}
	public double minor(int row, int column)
	{
		return submatrix(row, column).determinant();
	}
	public double cofactor(int row, int column)
	{
		return (row + column) % 2 == 0 ? minor(row, column) : -1 * minor(row, column);
	}
	public Matrix transpose()
	{
		Matrix copy = new Matrix(mbrColumns, mbrRows);
		for (int i = 0; i < copy.mbrRows; ++i)
		{
			for (int j = 0; j < copy.mbrColumns; ++j)
			{
				copy.setRC(i, j, getRC(j, i));
				//std::cout << "i:" << i << "j:" << j << "now: " << getRC(j, i) << "=" << copy->getRC(i, j) << std::endl;
			};
		};
		return copy;
	}
	public Matrix submatrix(int row, int column)
	{
		Matrix sub = new Matrix(mbrRows - 1, mbrColumns - 1);
		int subrow = 0;
		int subcol = 0;
		int i = 0;
		int j = 0;
		while (i < mbrRows)
		{
			if (i == row) { i += 1; }
			j = 0;
			subcol = 0;
			while (j < mbrColumns)
			{
				if (j == column) { j += 1; }
				sub.setRC(subrow, subcol, getRC(i, j));
				//std::cout << "i:" << subrow << "j:" << subcol << "now: " << getRC(i, j) << "=" << sub->getRC(subrow, subcol) << std::endl;
				j += 1;
				subcol += 1;
			}
			i += 1;
			subrow += 1;
		}
		return sub;
	}
	public Matrix getInverse()
	{
		// std::cout << "Matrix::invert()" << std::endl;
		Matrix inverse = new Matrix(mbrRows, mbrColumns);
		if (!CheckInvertible()) return inverse;
		double dm = determinant();
		for (int i = 0; i < mbrRows; ++i)
		{
			for (int j = 0; j < mbrColumns; ++j)
			{
				double cf = cofactor(i, j);
				inverse.setRC(j, i, cf / dm);
				// std::cout << "j:" << j << "i:" << i << "now: " << cf << "/" << dm << "=" << inverse->getRC(j, i) << std::endl;
			};
		};
		return inverse;
	}
	public Matrix identity()
	{
		Matrix identity = new Matrix(mbrRows, mbrColumns);
		for (int i = 0; i < identity.mbrRows; ++i)
		{
			for (int j = 0; j < identity.mbrColumns; ++j)
			{
				identity.mbrGrid[i][j] = (i == j ? 1 : 0);
			};
		};
		return identity;
	}
	public Matrix rotateX(double radians)
	{
		XRotationMatrix rotate = new XRotationMatrix(radians);
		return this * rotate;
	}
	public Matrix rotateY(double radians)
	{
		YRotationMatrix rotate = new YRotationMatrix(radians);
		return this * rotate;
	}
	public Matrix rotateZ(double radians)
	{
		ZRotationMatrix rotate = new ZRotationMatrix(radians);
		return this * rotate;
	}
	public Matrix scale(int x, int y, int z)
	{
		ScalingMatrix scale = new ScalingMatrix(x,y,z);
		return this * scale;
	}
	public Matrix translate(int x, int y, int z)
	{
		TranslationMatrix translate = new TranslationMatrix(x,y,z);
		return this * translate;
	}
	public Matrix shear(double xy, double xz, double yx, double yz, double zx, double zy)
	{
		ShearingMatrix shear = new ShearingMatrix(xy, xz, yx, yz, zx, zy);
		return this * shear;
	}
	public void RenderConsole() 
	{
		Console.WriteLine("Matrix::renderConsole()");
		for (int i = 0; i < mbrRows; ++i)
		{
			Console.Write("[");
			for (int j = 0; j < mbrColumns; ++j)
			{
				Console.Write($"{mbrGrid[i][j]}, ");
			};
			Console.WriteLine("]");
		};
	}
};
public class IdentityMatrix : Matrix {
	public IdentityMatrix(int rows, int columns) {
		for (int i = 0; i < mbrRows; ++i)
		{
			for (int j = 0; j < mbrColumns; ++j)
			{
				mbrGrid[i][j] = (i == j ?  1 : 0);
			};
		};
	}
};
public class TranslationMatrix : Matrix {
	public TranslationMatrix(double x, double y, double z) {
		mbrGrid[0][0] = 1;
		mbrGrid[0][3] = x;
		mbrGrid[1][3] = y;
		mbrGrid[1][1] = 1;
		mbrGrid[2][3] = z;
		mbrGrid[2][2] = 1;
		mbrGrid[3][3] = 1;
	}

};
public class ScalingMatrix : Matrix {
	public ScalingMatrix(double x, double y, double z) {
		mbrGrid[0][0] = x;
		mbrGrid[1][1] = y;
		mbrGrid[2][2] = z;
		mbrGrid[3][3] = 1;
	}
};
public class XRotationMatrix : Matrix {
	public XRotationMatrix(double radians) {
		mbrGrid[0][0] = 1;
		mbrGrid[1][1] = Math.Cos(radians);
		mbrGrid[1][2] = -Math.Sin(radians);
		mbrGrid[2][1] = Math.Sin(radians);
		mbrGrid[2][2] = Math.Cos(radians);
		mbrGrid[3][3] = 1;
	}
};
public class YRotationMatrix : Matrix {
	public YRotationMatrix(double radians) {
		mbrGrid[0][0] = Math.Cos(radians);
		mbrGrid[0][2] = Math.Sin(radians);
		mbrGrid[1][1] = 1;
		mbrGrid[2][0] = -Math.Sin(radians);
		mbrGrid[2][2] = Math.Cos(radians);
		mbrGrid[3][3] = 1;
	}
};
public class ZRotationMatrix : Matrix {
	public ZRotationMatrix(double radians) {
		mbrGrid[0][0] = Math.Cos(radians);
		mbrGrid[0][1] = -Math.Sin(radians);
		mbrGrid[1][0] = Math.Sin(radians);
		mbrGrid[1][1] = Math.Cos(radians);
		mbrGrid[2][2] = 1;
		mbrGrid[3][3] = 1;
	}
};
public class ShearingMatrix : Matrix {
	public ShearingMatrix(double xy, double xz, double yx, double yz, double zx, double zy) {
		mbrGrid[0][0] = 1;
		mbrGrid[0][1] = xy;
		mbrGrid[0][2] = xz;
		mbrGrid[1][0] = yx;
		mbrGrid[1][1] = 1;
		mbrGrid[1][2] = yz;
		mbrGrid[2][0] = zx;
		mbrGrid[2][1] = zy;
		mbrGrid[2][2] = 1;
		mbrGrid[3][3] = 1;
	}
};
public class ViewMatrix : Matrix {
	public ViewMatrix()
	{
		for (int i = 0; i < mbrRows; ++i)
		{
			for (int j = 0; j < mbrColumns; ++j)
			{
				mbrGrid[i][j] = (i == j ?  1 : 0);
			};
		};
	}
	public ViewMatrix(Point start, Point end, Vector up) {
		Vector varForward = (end - start).GetNormal();
		Vector varLeft = varForward.GetCrossProduct(up.GetNormal());
		Vector varUp = varLeft.GetCrossProduct(varForward);
		List<double> varValues = new List<double>{varLeft._fieldX, varLeft._fieldY, varLeft._fieldZ, 0, varUp._fieldX, varUp._fieldY, varUp._fieldZ, 0, -varForward._fieldX, -varForward._fieldY, -varForward._fieldZ, 0, 0, 0, 0, 1};
		Matrix varOrientation = new Matrix(4,4, varValues);
		Matrix varResult = varOrientation * new TranslationMatrix(-start._fieldX, -start._fieldY, -start._fieldZ);
		for (int i = 0; i < mbrRows; ++i)
		{
			for (int j = 0; j < mbrColumns; ++j)
			{
				mbrGrid[i][j] = varResult.mbrGrid[i][j];
				// std::cout << "addresses this:" << & grid[i][j] << ":other:" << &varResult->grid[i][j] << std::endl;
				// std::cout << "values this:" << grid[i][j] << ":other:" << varResult->grid[i][j] << std::endl;
			};
		};
	}
};