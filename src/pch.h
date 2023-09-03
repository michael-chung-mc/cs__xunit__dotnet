//
// pch.h
//

#pragma once

#include "gtest/gtest.h";
#include "tuple.h";
#include "comparinator.h";
#include "color.h";
#include "canvas.h";

#include <iostream>;
#include <fstream>;

inline std::string getPPMFilename() {
	return "./data/image.ppm";
}
inline int getPPMLineWidth() {
	return 70;
}