using LibColor;
using LibComparinator;
using LibProjectMeta;
namespace LibCanvas;
public class Canvas {
	public int mbrWidth;
	public int mbrHeight;
	public List<List<Color>> mbrGrid;
	public Canvas(int width, int height) {
		mbrWidth = width;
		mbrHeight = height;
		mbrGrid.Clear();
		for (int i = 0; i < mbrWidth; ++i)
		{
			mbrGrid.Add(new List<Color>());
			for (int j = 0; j < mbrHeight; ++j)
			{
				mbrGrid[i].Add(new Color(0, 0, 0));
			}
		}
	}
	public Color getPixel(int x, int y) {
		if (inBounds(x, y)) { return mbrGrid[x][y]; }
		return new Color(0,0,0);
	}
	public String getPPM()
	{
		ProjectMeta varPM = new ProjectMeta();
		//String ppm = "P3\n" + std::to_string(w) + " " + std::to_string(h) + "\n" + "255\n";
		String ppm = "P3\n" + mbrWidth.ToString() + " " + mbrHeight.ToString() + "\n" + "255\n";
		String buffer = "";
		int cnr = 0;
		int cng = 0;
		int cnb = 0;
		// width = row & height = column
		for (int j = 0; j < mbrHeight; ++j)
		{
			for (int i = 0; i < mbrWidth; ++i)
			{
				cnr = Math.Min((int)Math.Round((255 * mbrGrid[i][j]._fieldRed)), 255);
				cnr = cnr > 0 ? cnr : 0;
				String clampedNormalizedRed = cnr.ToString();
				if (buffer.Count() + clampedNormalizedRed.Count() > varPM.getPPMLineWidth())
				{
					ppm += buffer;
					ppm = char.IsWhiteSpace(ppm[ppm.Count() - 1]) ? ppm.Substring(0, ppm.Count() - 1) : ppm;
					buffer = "\n" + clampedNormalizedRed + " ";
				}
				else
				{
					buffer += clampedNormalizedRed + " ";
				}
				cng = Math.Min((int)Math.Round((255 * mbrGrid[i][j]._fieldGreen)), 255);
				cng = cng > 0 ? cng : 0;
				String clampedNormalizedGreen = cng.ToString();
				if (buffer.Count() + clampedNormalizedGreen.Count() > varPM.getPPMLineWidth())
				{
					ppm += buffer;
					ppm = char.IsWhiteSpace(ppm[ppm.Count() - 1]) ? ppm.Substring(0, ppm.Count() - 1) : ppm;
					buffer = "\n" + clampedNormalizedGreen + " ";
				}
				else
				{
					buffer += clampedNormalizedGreen + " ";
				}
				cnb = Math.Min((int)Math.Round((255 * mbrGrid[i][j]._fieldBlue)), 255);
				cnb = cnb > 0 ? cnb : 0;
				String clampedNormalizedBlue = cnb.ToString();
				if (buffer.Count() + clampedNormalizedBlue.Count() > varPM.getPPMLineWidth())
				{
					ppm += buffer;
					ppm = char.IsWhiteSpace(ppm[ppm.Count() - 1]) ? ppm.Substring(0, ppm.Count() - 1) : ppm;
					buffer = "\n" + clampedNormalizedBlue + " ";
				}
				else
				{
					buffer += clampedNormalizedBlue + " ";
				}
			}
			ppm += buffer;
			ppm = char.IsWhiteSpace(ppm[ppm.Count() - 1]) ? ppm.Substring(0, ppm.Count() - 1) : ppm;
			buffer = "\n";
			//Console.WriteLine("rendered line:" << std::to_string(j));
		}
		// cutoff extra whitespace
		ppm = char.IsWhiteSpace(ppm[ppm.Count() - 1]) ? ppm.Substring(0, ppm.Count() - 1) : ppm;
		ppm += "\n";
		return ppm;
	}
	public bool isClean() {
		Comparinator ce = new Comparinator();
		Color black = new Color(0, 0, 0);
		bool clean = true;
		for (int i = 0; i < mbrGrid.Count(); ++i)
		{
			for (int j = 0; j < mbrGrid[0].Count(); ++j)
			{
				if (!ce.CheckTuple(mbrGrid[i][j], black))
				{
					clean = false;
					break;
				}
			}
		}
		return clean;
	}
	public bool inBounds(int x, int y) {
		return mbrGrid.Count() > 0 && mbrGrid[0].Count() > 0 && x >= 0 && x < mbrGrid.Count() && y >= 0 && y < mbrGrid[0].Count();
	}
	public void setPixel(int x, int y, Color c) {
		if (inBounds(x,y)) { mbrGrid[x][y] = c; }
	}
	public void fill(Color x)  {
		for (int i = 0; i < mbrGrid.Count(); ++i)
		{
			for (int j = 0; j < mbrGrid[i].Count(); ++j)
			{
				setPixel(i, j, x);
			}
		}
	}
	public void save(){
		ProjectMeta varPm = new ProjectMeta();
		// std::ofstream file(varPm.getPPMFilename(true));
		// Console.WriteLine("to saving ...");
		// String data = getPPM();
		// if (file.is_open()) {
		// 	file << data;
		// }
		// file.close();
		using (StreamWriter writer = new StreamWriter(varPm.getPPMFilename(true))) {
			writer.WriteLine(getPPM());
		}
		Console.WriteLine("saved");
	}
};