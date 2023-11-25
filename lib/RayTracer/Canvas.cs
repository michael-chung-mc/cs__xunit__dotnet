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
		mbrGrid = new List<List<Color>>();
		for (int i = 0; i < mbrWidth; ++i)
		{
			mbrGrid.Add(new List<Color>());
			for (int j = 0; j < mbrHeight; ++j)
			{
				mbrGrid[i].Add(new Color(0, 0, 0));
			}
		}
	}
	public Color GetPixel(int x, int y) {
		if (CheckBounds(x, y)) { return mbrGrid[x][y]; }
		return new Color(0,0,0);
	}
	public String RenderStringPPMP3()
	{
		ProjectMeta varPM = new ProjectMeta();
		String ppm = "P3\n" + mbrWidth.ToString() + " " + mbrHeight.ToString() + "\n" + "255\n";
		String buffer = "";
		int cnr = 0;
		int cng = 0;
		int cnb = 0;
		for (int j = 0; j < mbrHeight; ++j)
		{
			for (int i = 0; i < mbrWidth; ++i)
			{
				cnr = Math.Min((int)Math.Round((255 * mbrGrid[i][j]._fieldRed)), 255);
				cnr = cnr > 0 ? cnr : 0;
				String clampedNormalizedRed = cnr.ToString();
				if (buffer.Count() + clampedNormalizedRed.Count() > varPM.GetPPMLineWidth())
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
				if (buffer.Count() + clampedNormalizedGreen.Count() > varPM.GetPPMLineWidth())
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
				if (buffer.Count() + clampedNormalizedBlue.Count() > varPM.GetPPMLineWidth())
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
		}
		// cutoff extra whitespace
		ppm = char.IsWhiteSpace(ppm[ppm.Count() - 1]) ? ppm.Substring(0, ppm.Count() - 1) : ppm;
		ppm += "\n";
		return ppm;
	}
	public bool CheckClean() {
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
	public bool CheckBounds(int x, int y) {
		return mbrGrid.Count() > 0 && mbrGrid[0].Count() > 0 && x >= 0 && x < mbrGrid.Count() && y >= 0 && y < mbrGrid[0].Count();
	}
	public void SetPixel(int x, int y, Color c) {
		if (CheckBounds(x,y)) { mbrGrid[x][y] = c; }
	}
	public void SetFill(Color x)  {
		for (int i = 0; i < mbrGrid.Count(); ++i)
		{
			for (int j = 0; j < mbrGrid[i].Count(); ++j)
			{
				SetPixel(i, j, x);
			}
		}
	}
	public void RenderFile(String argName = ""){
		ProjectMeta varPm = new ProjectMeta();
		String varFileName = varPm.getPPMFilename(argName);
		Console.WriteLine($"Saving to: {varFileName}");
		using (StreamWriter writer = new StreamWriter(varFileName)) {
			writer.WriteLine(RenderStringPPMP3());
		}
		Console.WriteLine("saved");
	}
};