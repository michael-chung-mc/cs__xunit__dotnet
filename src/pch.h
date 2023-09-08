//
// pch.h
//

#pragma once

#include "gtest/gtest.h";
#include "tuple.h";
#include "comparinator.h";
#include "color.h";
#include "canvas.h";

#include <iomanip>;
#include <ctime>;
#include <iostream>;
#include <fstream>;

inline std::string getPPMFilename() {
	std::time_t now = std::time(nullptr);
	std::tm ltm = *std::localtime(&now);
	std::stringstream ss;
	ss << std::put_time(&ltm, "%Y%m%d_%H%M%S");
	std::string time = ss.str();
	std::string path = "./data/" + time + ".ppm";
	std::cout << path << std::endl;
	return path;
}
inline int getPPMWidth() {
	return 40;
	//return 1920;
}
inline int getPPMHeight() {
	return 20;
	//return 1080;
}
inline int getPPMLineWidth() {
	return 70;
}