class Matrix {
public:
	double **grid;
	int rnum;
	int cnum;
	Matrix(int row, int column);
	Matrix(Matrix& other);
	~Matrix();
	Matrix& operator=(const Matrix other);
	bool operator==(const Matrix other);
	Matrix* operator*(const Matrix other);
	Tuple operator*(const Tuple other);
	bool checkValid(int row, int column);
	bool checkEqual(const Matrix other);
	void setRC(int row, int column, double value);
	double getRC(int row, int column);
};