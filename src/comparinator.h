#pragma once
#ifndef COMPARINATOR_H
#define COMPARINATOR_H

class Comparinator {
private:
	float EPSILON = 0.00001;
public:
	bool checkFloat(const double &a, const double &b) const;
	// tuples & point & vector
	template <typename Agent>
	bool checkTuple(Agent a, Agent b)
	{
		return (checkFloat(a.x, b.x) && checkFloat(a.y, b.y) && checkFloat(a.z, b.z) && checkFloat(a.w, b.w));
	};
};

#endif