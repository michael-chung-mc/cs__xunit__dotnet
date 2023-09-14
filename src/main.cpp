#include "pch.h"
#include "projectile.cpp"

int main()
{
	double arr[] = { 2, 0, 0, 0, 0, 1, 0, 0, 0,0,1,0 ,0 ,0 ,0, 1 };
	Matrix m = Matrix(4, 4, arr);
	Tuple t = Tuple(2, 2, 2, 2);
	Tuple x = m * t;
//	int firepower = 10;
//	std::cout << "Firing: " << firepower << std::endl;
//	Simulation sim;
//	sim.fire(firepower);
//	return 0;
}