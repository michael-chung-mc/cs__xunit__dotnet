#include "pch.h"
#include <iostream>
#include <fstream>

Canvas::Canvas(int width, int height) {
	w = width;
	h = height;
	grid.clear();
	for (int i = 0; i < w; i++)
	{
		std::vector<Color> column;
		grid.push_back(column);
		for (int j = 0; j < h; j++)
		{
			grid[i].push_back(Color(0, 0, 0));
		}
	}
};

bool Canvas::isClean() {
	Comparinator ce = Comparinator();
	Color black = Color(0, 0, 0);
	bool clean = true;
	for (int i = 0; i < grid.size(); i++)
	{
		for (int j = 0; j < grid[0].size(); j++)
		{
			if (!ce.equalTuple(grid[i][j], black))
			{
				clean = false;
				break;
			}
		}
	}
	return clean;
}

bool Canvas::inBounds(int x,int y) {
	return grid.size() > 0 && grid[0].size() > 0 && x >= 0 && x < grid.size() && y >= 0 && y < grid[0].size();
}

void Canvas::setPixel(int x, int y, Color c) {
	if (inBounds(x,y)) { grid[x][y] = c; }
}

Color Canvas::getPixel(int x, int y) {
	if (inBounds(x, y)) { return grid[x][y]; }
}

void Canvas::save() {
	std::ofstream file;
	file.open(FILENAME);
	file << "P3" << std::endl;
	file << w << " " << h << std::endl;
	file << "255" << std::endl;
	file.close();
}