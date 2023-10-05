#pragma once
#ifndef COMPARINATOR_H
#define COMPARINATOR_H

class Comparinator {
public:
	Comparinator();
	bool checkFloat(const double &a, const double &b) const;
	// tuples & point & vector
	template <typename Agent>
	bool checkTuple(Agent a, Agent b)
	{
		return (checkFloat(a.argX, b.argX) && checkFloat(a.argY, b.argY) && checkFloat(a.argZ, b.argZ) && checkFloat(a.argW, b.argW));
	};
private:
	float mbrEpsilon;
};

#endif