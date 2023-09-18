#pragma once
#ifndef Intersection_H
#define Intersection_H

class Intersection
{
public:
	double time;
	Sphere object;
	Intersection(double time, Sphere s);
};

#endif