class Matrix {
public:
	double **grid;
	Matrix(int row, int column);
	~Matrix();
	bool checkValid(int row, int column);
	void setRC(int row, int column, double value);
	double getRC(int row, int column);
private:
	int rnum;
	int cnum;
};