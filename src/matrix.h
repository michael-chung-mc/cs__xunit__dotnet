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
	bool checkValid(int row, int column);
	void setRC(int row, int column, double value);
	double getRC(int row, int column);
};