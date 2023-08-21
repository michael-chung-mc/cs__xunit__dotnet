#include "pch.h"
#pragma once
#ifndef Tuples_H
#define Tuples_H

class Tuples {
public:
	double x;
	double y;
	double z;
	double w;
	Tuples(double argx, double argy, double argz, double argw);
	Tuples add(Tuples a);
};
#endif