using Xunit;
using LibRay;
using LibTuple;
using LibMatrix;
using LibComparinator;

namespace LibRay.Test;
public class RayTest
{
	Comparinator _fieldComp = new Comparinator();
	[Fact]
	public void CanaryTest() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}
	[Fact]
	public void RayCtor() {
		Point p = new Point(1, 2, 3);
		Vector d = new Vector(4, 5, 6);
		Ray r = new Ray(p, d);
		Assert.True(_fieldComp.CheckTuple(r._fieldOrigin,p));
		Assert.True(_fieldComp.CheckTuple(r._fieldDirection,d));
	}
	[Fact]
	public void RayPositionAfterTime() {
		Point p = new Point(2, 3, 4);
		Vector d = new Vector(1, 0, 0);
		Ray r = new Ray(p, d);
		Point pos0 = r.GetPosition(0);
		Point expos0 = new Point(2, 3, 4);
		Point pos1 = r.GetPosition(1);
		Point expos1 = new Point(3, 3, 4);
		Point posn1 = r.GetPosition(-1);
		Point exposn1 = new Point(1, 3, 4);
		Point pos2p5 = r.GetPosition(2.5);
		Point expos2p5 = new Point(4.5, 3, 4);
		Assert.True(_fieldComp.CheckTuple(pos0,expos0));
		Assert.True(_fieldComp.CheckTuple(pos1, expos1));
		Assert.True(_fieldComp.CheckTuple(posn1, exposn1));
		Assert.True(_fieldComp.CheckTuple(pos2p5, expos2p5));
	}
	[Fact]
	public void RayTranslation() {
		Point p = new Point(1, 2, 3);
		Vector d = new Vector(0, 1, 0);
		Ray r = new Ray(p, d);
		TranslationMatrix m = new TranslationMatrix(3, 4, 5);
		Point expectedP = new Point(4, 6, 8);
		Vector expectedV = new Vector(0,1,0);
		Ray t = r.Transform(m);
		Assert.True(_fieldComp.CheckTuple(t._fieldOrigin, expectedP));
		Assert.True(_fieldComp.CheckTuple(t._fieldDirection, expectedV));
		Assert.True(_fieldComp.CheckTuple(r._fieldOrigin, p));
		Assert.True(_fieldComp.CheckTuple(r._fieldDirection, d));
	}
	[Fact]
	public void RayScaling() {
		Point p = new Point(1, 2, 3);
		Vector d = new Vector(0, 1, 0);
		Ray r = new Ray(p, d);
		ScalingMatrix m = new ScalingMatrix(2, 3, 4);
		Point expectedP = new Point(2, 6, 12);
		Vector expectedV = new Vector(0,3,0);
		Ray t = r.Transform(m);
		Assert.True(_fieldComp.CheckTuple(t._fieldOrigin, expectedP));
		Assert.True(_fieldComp.CheckTuple(t._fieldDirection, expectedV));
		Assert.True(_fieldComp.CheckTuple(r._fieldOrigin, p));
		Assert.True(_fieldComp.CheckTuple(r._fieldDirection, d));
	}
}