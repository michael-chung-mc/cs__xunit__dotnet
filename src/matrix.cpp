#include "pch.h"

Matrix::Matrix(int rows, int columns)
{
	rnum = rows;
	cnum = columns;
	this->grid = new double* [rnum];
	for (int i = 0; i < rnum; i++)
	{
		this->grid[i] = new double[cnum];
	}
	for (int i = 0; i < rnum; i++)
	{
		for (int j = 0; j < cnum; j++)
		{
			this->grid[i][j] = 0;
		}
	}
}

Matrix::Matrix(Matrix& other)
{
	//std::cout << "matrix copy ctor" << std::endl;
	this->rnum = other.rnum;
	this->cnum = other.cnum;
	//std::cout << "this: " << rnum << cnum << " other:" << other.rnum << other.cnum << std::endl;
	this->grid = new double* [rnum];
	for (int i = 0; i < rnum; i++)
	{
		this->grid[i] = new double[cnum];
	}
	for (int i = 0; i < rnum; i++)
	{
		for (int j = 0; j < cnum; j++)
		{
			this->grid[i][j] = other.grid[i][j];
			//std::cout << "this: " << grid[i][j] << " other:" << other.grid[i][j] << std::endl;
		}
	}
}

Matrix::Matrix(int rows, int columns, double* values)
{
	rnum = rows;
	cnum = columns;
	this->grid = new double* [rnum];
	for (int i = 0; i < rnum; i++)
	{
		this->grid[i] = new double[cnum];
	}
	for (int i = 0; i < rnum; i++)
	{
		for (int j = 0; j < cnum; j++)
		{
			this->grid[i][j] = values[(i * columns) + j];
		}
	}
}

Matrix::~Matrix()
{
	//std::cout << "deleting matrix" << &grid << std::endl;
	for (int i = 0; i < rnum; i++)
	{
		delete[] this->grid[i];
		this->grid[i] = nullptr;
	}
	delete[] this->grid;
	this->grid = nullptr;
}

Matrix& Matrix::operator=(Matrix other)
{
	//std::cout << "matrix copy assign" << std::endl;
	if (this == &other) return *this;
	this->rnum = other.rnum;
	this->cnum = other.cnum;
	for (int i = 0; i < rnum; i++)
	{
		delete[] this->grid[i];
		this->grid[i] = nullptr;
	}
	delete[] this->grid;
	this->grid = nullptr;
	this->grid = new double* [rnum];
	for (int i = 0; i < rnum; i++)
	{
		this->grid[i] = new double[cnum];
	}
	for (int i = 0; i < rnum; i++)
	{
		for (int j = 0; j < cnum; j++)
		{
			this->grid[i][j] = other.grid[i][j];
		}
	}
	return *this;
}

bool Matrix::operator==(const Matrix other)
{
	//std::cout << "matrix equality operator" << std::endl;
	if (other.rnum != rnum || other.cnum != cnum)
	{
		return false;
	}
	for (int i = 0; i < rnum; i++)
	{
		for (int j = 0; j < cnum; j++)
		{
			//std::cout << "addresses this:" << &grid[i][j] << ":other:" << &other.grid[i][j] << std::endl;
			//std::cout << "values this:" << grid[i][j] << ":other:" << other.grid[i][j] << std::endl;
			if (other.grid[i][j] != this->grid[i][j])
			{
				return false;
			}
		}
	}
	return true;
}

bool Matrix::checkEqual(const Matrix other)
{
	//std::cout << "matrix equality" << std::endl;
	if (other.rnum != rnum || other.cnum != cnum)
	{
		return false;
	}
	Comparinator ce = Comparinator();
	//std::cout << "row == column" << std::endl;
	for (int i = 0; i < rnum; i++)
	{
		for (int j = 0; j < cnum; j++)
		{
			std::cout << "addresses this:" << & grid[i][j] << ":other:" << &other.grid[i][j] << std::endl;
			std::cout << "values this:" << grid[i][j] << ":other:" << other.grid[i][j] << std::endl;
			if (!ce.checkFloat(other.grid[i][j],this->grid[i][j]))
			{
				return false;
			}
		}
	}
	return true;
}

Matrix* Matrix::operator*(const Matrix other)
{
	//std::cout << "matrix multiplication" << std::endl;
	Matrix* result = new Matrix(other.rnum, other.cnum);
	for (int i = 0; i < result->rnum; i++)
	{
		for (int j = 0; j < result->cnum; j++)
		{
			double value = 0;
			for (int k = 0; k < result->cnum; k++)
			{
				//std::cout << "i:" << i << "j:" << j << "k:" << k << "=" << grid[i][k] << "*" << other.grid[k][j] << std::endl;
				value += grid[i][k] * other.grid[k][j];
			}
			//std::cout << value << std::endl;
			result->setRC(i, j, value);
		}
	}
	return result;
}

Tuple Matrix::operator*(const Tuple other)
{
	if (rnum != 4 && cnum != 4) return other;
	//std::cout << "matrix tuple multiplication" << std::endl;
	double pseudoMatrix[4] = {other.x, other.y, other.z, other.w};
	double results[4];
	double res = 0;
	for (int tuple = 0; tuple < 4; tuple++)
	{
		res = 0;
		for (int col = 0; col < cnum; col++)
		{
			//std::cout << "i:" << col << "j:" << tuple << "k:" << res << "=" << pseudoMatrix[col] << "*" << grid[tuple][col] << std::endl;
			res += grid[tuple][col] * pseudoMatrix[col];
		}
		results[tuple] = res;
	}
	return Tuple(results[0],results[1],results[2],results[3]);
}
Point Matrix::operator*(const Point other)
{
	Tuple tp = Tuple(other.x, other.y, other.z, other.w);
	Tuple res = *(this) * tp;
	return Point(res.x, res.y, res.z);
}
Vector Matrix::operator*(const Vector other)
{
	Tuple tp = Tuple(other.x, other.y, other.z, other.w);
	Tuple res = *(this) * tp;
	return Vector(res.x, res.y, res.z);
}

bool Matrix::checkInvertible() {
	return determinant() != 0;
}
bool Matrix::checkValid(int row, int column)
{
	return row >= 0 && row < rnum && column >= 0 && column < cnum;
}

double Matrix::getRC(int row, int column)
{
	if (checkValid(row, column))
	{
		return this->grid[row][column];
	}
}

void Matrix::setRC(int row, int column, double value)
{
	if (checkValid(row, column))
	{
		this->grid[row][column] = value;
	}
}

Matrix* Matrix::transpose()
{
	Matrix* copy = new Matrix(cnum, rnum);
	for (int i = 0; i < copy->rnum; i++)
	{
		for (int j = 0; j < copy->cnum; j++)
		{
			copy->setRC(i, j, this->getRC(j, i));
			//std::cout << "i:" << i << "j:" << j << "now: " << this->getRC(j, i) << "=" << copy->getRC(i, j) << std::endl;
		};
	};
	return copy;
}

double Matrix::determinant()
{
	double determinant = 0;
	if (rnum == 2 && cnum == 2) determinant = grid[0][0] * grid[1][1] - grid[0][1] * grid[1][0];
	else
	{
		for (int i = 0; i < cnum; i++)
		{
			determinant += cofactor(0, i) * getRC(0, i);
		};
	};
	return determinant;
}

Matrix* Matrix::submatrix(int row, int column)
{
	Matrix* sub = new Matrix(rnum - 1, cnum - 1);
	int subrow = 0;
	int subcol = 0;
	int i = 0;
	int j = 0;
	while (i < rnum)
	{
		if (i == row) { i += 1; }
		j = 0;
		subcol = 0;
		while (j < cnum)
		{
			if (j == column) { j += 1; }
			sub->setRC(subrow, subcol, this->getRC(i, j));
			//std::cout << "i:" << subrow << "j:" << subcol << "now: " << this->getRC(i, j) << "=" << sub->getRC(subrow, subcol) << std::endl;
			j += 1;
			subcol += 1;
		}
		i += 1;
		subrow += 1;
	}
	return sub;
}

double Matrix::minor(int row, int column)
{
	return this->submatrix(row, column)->determinant();
}

double Matrix::cofactor(int row, int column)
{
	return (row + column) % 2 == 0 ? this->minor(row, column) : -1 * this->minor(row, column);
}

Matrix* Matrix::invert()
{
	Matrix* inverse = new Matrix(rnum, cnum);
	if (!checkInvertible()) return inverse;
	double dm = this->determinant();
	for (int i = 0; i < this->rnum; i++)
	{
		for (int j = 0; j < this->cnum; j++)
		{
			double cf = this->cofactor(i, j);
			inverse->setRC(j, i, cf / dm);
			std::cout << "j:" << j << "i:" << i << "now: " << cf << "/" << dm << "=" << inverse->getRC(j, i) << std::endl;
		};
	};
	return inverse;
}
Matrix* Matrix::identity()
{
	Matrix* identity = new Matrix(rnum, cnum);
	for (int i = 0; i < identity->rnum; i++)
	{
		for (int j = 0; j < identity->cnum; j++)
		{
			i == j ? identity->grid[i][j] = 1 : identity->grid[i][j] = 0;
		};
	};
	return identity;
}
Matrix* Matrix::rotateX(double radians)
{
	XRotationMatrix rotate = XRotationMatrix(radians);
	return *this * rotate;
}
Matrix* Matrix::rotateY(double radians)
{
	YRotationMatrix rotate = YRotationMatrix(radians);
	return *this * rotate;
}
Matrix* Matrix::rotateZ(double radians)
{
	ZRotationMatrix rotate = ZRotationMatrix(radians);
	return *this * rotate;
}
Matrix* Matrix::scale(int x, int y, int z)
{
	ScalingMatrix scale = ScalingMatrix(x,y,z);
	return *this * scale;
}
Matrix* Matrix::translate(int x, int y, int z)
{
	TranslationMatrix translate = TranslationMatrix(x,y,z);
	return *this * translate;
}
Matrix* Matrix::shear(double xy, double xz, double yx, double yz, double zx, double zy)
{
	ShearingMatrix shear = ShearingMatrix(xy, xz, yx, yz, zx, zy);
	return *this * shear;
}


IdentityMatrix::IdentityMatrix (int rows, int columns) : Matrix(rows = rows, columns = columns)
{
	for (int i = 0; i < rnum; i++)
	{
		for (int j = 0; j < cnum; j++)
		{
			i == j ? this->grid[i][j] = 1 : this->grid[i][j] = 0;
		};
	};
}

TranslationMatrix::TranslationMatrix(int x, int y, int z) : Matrix(4, 4)
{
	this->grid[0][0] = 1;
	this->grid[0][3] = x;
	this->grid[1][3] = y;
	this->grid[1][1] = 1;
	this->grid[2][3] = z;
	this->grid[2][2] = 1;
	this->grid[3][3] = 1;
}

ScalingMatrix::ScalingMatrix(int x, int y, int z) : Matrix(4, 4)
{
	this->grid[0][0] = x;
	this->grid[1][1] = y;
	this->grid[2][2] = z;
	this->grid[3][3] = 1;
}

XRotationMatrix::XRotationMatrix(double radians) : Matrix(4, 4)
{
	this->grid[0][0] = 1;
	this->grid[1][1] = cos(radians);
	this->grid[1][2] = -sin(radians);
	this->grid[2][1] = sin(radians);
	this->grid[2][2] = cos(radians);
	this->grid[3][3] = 1;
}

YRotationMatrix::YRotationMatrix(double radians) : Matrix(4, 4)
{
	this->grid[0][0] = cos(radians);
	this->grid[0][2] = sin(radians);
	this->grid[1][1] = 1;
	this->grid[2][0] = -sin(radians);
	this->grid[2][2] = cos(radians);
	this->grid[3][3] = 1;
}

ZRotationMatrix::ZRotationMatrix(double radians) : Matrix(4, 4)
{
	this->grid[0][0] = cos(radians);
	this->grid[0][1] = -sin(radians);
	this->grid[1][0] = sin(radians);
	this->grid[1][1] = cos(radians);
	this->grid[2][2] = 1;
	this->grid[3][3] = 1;
}

ShearingMatrix::ShearingMatrix(double xy, double xz, double yx, double yz, double zx, double zy) : Matrix(4, 4)
{
	this->grid[0][0] = 1;
	this->grid[0][1] = xy;
	this->grid[0][2] = xz;
	this->grid[1][0] = yx;
	this->grid[1][1] = 1;
	this->grid[1][2] = yz;
	this->grid[2][0] = zx;
	this->grid[2][1] = zy;
	this->grid[2][2] = 1;
	this->grid[3][3] = 1;
}