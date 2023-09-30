//
// pch.h
//

#pragma once

#include <gtest/gtest.h>

#include <iomanip>
#include <ctime>
#include <iostream>
#include <fstream>
#include <cmath>

#include "tuple.h"
#include "comparinator.h"
#include "color.h"
#include "canvas.h"
#include "matrix.h"
#include "ray.h"
#include "sphere.h"
#include "intersection.h"
#include "light.h"

inline std::string getPPMFilename(bool linuxPath) {
	std::time_t now = std::time(nullptr);
	std::tm ltm = *std::localtime(&now);
	std::stringstream ss;
	ss << std::put_time(&ltm, "%Y%m%d_%H%M%S");
	std::string time = ss.str();
	std::string dirWindows = "./data/";
	std::string dirLinux = ".\\data\\";
	std::string dir = linuxPath ? dirLinux : dirWindows;
	std::string path = dir + time + ".ppm";
	std::cout << path << std::endl;
	return path;
}
inline int getPPMWidth() {
	return 720;
}
inline int getPPMHeight() {
	return 480;
}
inline int getPPMLineWidth() {
	return 70;
}
inline double getPI()
{
	return 3.141592653589793238463;
}