#include "pch.h"

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

std::string Canvas::getPPM()
{
	std::string ppm = "P3\n" + std::to_string(w) + " " + std::to_string(h) + "\n" + "255\n";
	for (int i = 0; i < grid.size(); i++)
	{
		for (int j = 0; j < grid[0].size(); j++)
		{
			int clampedNormalizedRed = std::min((int)std::round((255 * grid[i][j].r)), 255);
			clampedNormalizedRed = clampedNormalizedRed > 0 ? clampedNormalizedRed : 0;
			int clampedNormalizedGreen = std::min((int)std::round((255 * grid[i][j].g)), 255);
			clampedNormalizedGreen = clampedNormalizedGreen > 0 ? clampedNormalizedGreen : 0;
			int clampedNormalizedBlue = std::min((int)std::round((255 * grid[i][j].b)), 255);
			clampedNormalizedBlue = clampedNormalizedBlue > 0 ? clampedNormalizedBlue : 0;
			ppm += std::to_string(clampedNormalizedRed) + ' ' + std::to_string(clampedNormalizedGreen) + ' ' + std::to_string(clampedNormalizedBlue) + ' ';
		}
	}
	// cutoff extra whitespace
	return ppm.substr(0,ppm.size()-1);
}

void Canvas::save() {
	std::ofstream file;
	file.open(getImageFilename());
	file << getPPM();
	file.close();
}