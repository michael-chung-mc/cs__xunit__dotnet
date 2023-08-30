#include "pch.h"
#include "projectile.cpp"

int main()
{
	int firepower = 50;
	std::cout << "Firing: " << firepower << std::endl;
	Simulation sim;
	sim.fire(firepower);
	return 0;
}