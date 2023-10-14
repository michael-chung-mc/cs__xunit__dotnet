using Xunit;
using LibColor;
using LibCanvas;
using LibComparinator;
namespace LibCanvas.Test;

public class CanvasTest {
	Comparinator _fieldComp = new Comparinator();
    [Fact]
    public void Canary()
    {
        Assert.Equal(1, 1);
    }
	[Fact]
	public void CanvasInit ()
	{
		//Scenario: Creating a canvas
		//Given c ← new Canvas(10, 20)
		//Then c.width = 10
		//And c.height = 20
		//And every pixel of c is new Color(0, 0, 0)
		Canvas c = new Canvas(10,20);
		Assert.Equal(10, c.mbrWidth);
		Assert.Equal(20, c.mbrHeight);
		Assert.True(c.CheckClean());
	}
	[Fact]
	public void CanvasSetColor ()
	{
		//Scenario: Writing pixels to a canvas
		//Given c ← new Canvas(10, 20)
		//And red ← new Color(1, 0, 0)
		//When write_pixel(c, 2, 3, red)
		//Then pixel_at(c, 2, 3) = red
		Canvas c = new Canvas(10, 20);
		Color red = new Color(1, 0, 0);
		c.SetPixel(2, 3, red);
		Color test = c.GetPixel(2, 3);
		Assert.True(test.CheckEqual(red));
	}
	[Fact]
	public void CanvasPPM ()
	{
		//Scenario: Constructing the PPM header
		//And c1 ← new Color(1.5, 0, 0)
		//And c2 ← new Color(0, 0.5, 0)
		//And c3 ← new Color(-0.5, 0, 1)
		//When write_pixel(c, 0, 0, c1)
		//And write_pixel(c, 2, 1, c2)
		//And write_pixel(c, 4, 2, c3)
		//And ppm ← canvas_to_ppm(c)
		Canvas c = new Canvas(5, 3);
		Assert.True(c.CheckClean());
		Color c1 = new Color(1.5, 0, 0);
		Color c2 = new Color(0, 0.5, 0);
		Color c3 = new Color(-.5, 0, 1);
		c.SetPixel(0, 0, c1);
		c.SetPixel(2, 1, c2);
		c.SetPixel(4, 2, c3);
		String ppm = c.RenderStringPPMP3();
		String cppm = "P3\n5 3\n255\n255 0 0 0 0 0 0 0 0 0 0 0 0 0 0\n0 0 0 0 0 0 0 128 0 0 0 0 0 0 0\n0 0 0 0 0 0 0 0 0 0 0 0 0 0 255\n";
		Assert.Equal(ppm, cppm);
	}

	public void CanvasPPMLength ()
	{
		//Scenario: Splitting long lines in PPM files
		//Given c ← new Canvas(10, 2)
		//When every pixel of c is set to new Color(1, 0.8, 0.6)
		//And ppm ← canvas_to_ppm(c)
		Canvas c = new Canvas(10, 2);
		Color c1 = new Color(1, 0.8, 0.6);
		c.SetFill(c1);
		String ppm = c.RenderStringPPMP3();
		String cppm = "P3\n10 2\n255\n255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204\n153 255 204 153 255 204 153 255 204 153 255 204 153\n255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204\n153 255 204 153 255 204 153 255 204 153 255 204 153\n";
		Assert.Equal(ppm, cppm);
	}
}