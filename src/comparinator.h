#pragma once
#ifndef Comparinator_H
#define Comparinator_H

class Comparinator {
private:
	float EPSILON = 0.00001;
public:
	bool checkFloat(float a, float b);
	// tuples & point & vector
	template <typename Agent>
	bool checkTuple(Agent a, Agent b)
	{
		return (Comparinator::checkFloat(a.x, b.x) && Comparinator::checkFloat(a.y, b.y) && Comparinator::checkFloat(a.z, b.z) && Comparinator::checkFloat(a.w, b.w));
	};
};

#endif