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
	//std::cout << "row == column" << std::endl;
	for (int i = 0; i < rnum; i++)
	{
		for (int j = 0; j < cnum; j++)
		{
			std::cout << "addresses this:" << & grid[i][j] << ":other:" << &other.grid[i][j] << std::endl;
			std::cout << "values this:" << grid[i][j] << ":other:" << other.grid[i][j] << std::endl;
			if (other.grid[i][j] != this->grid[i][j])
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
		}
	}
	return copy;
}

double Matrix::determinant()
{
	return grid[0][0] * grid[1][1] - grid[0][1] * grid[1][0];
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
			std::cout << "i:" << subrow << "j:" << subcol << "now: " << this->getRC(i, j) << "=" << sub->getRC(subrow, subcol) << std::endl;
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

IdentityMatrix::IdentityMatrix (int rows, int columns) : Matrix(rows = rows, columns = columns)
{
	for (int i = 0; i < rnum; i++)
	{
		for (int j = 0; j < cnum; j++)
		{
			i==j ? this->grid[i][j] = 1 :  this->grid[i][j] = 0;
		}
	}
}