#include "pch.h"
#include <iostream>
#include "projectile.cpp"

int t()
{
	int firepower = 50;
	std::cout << "Firing: " << firepower << std::endl;
	Simulation sim;
	sim.fire(firepower);
	return 0;
}