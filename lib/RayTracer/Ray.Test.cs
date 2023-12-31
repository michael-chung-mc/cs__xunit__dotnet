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
	public void RayTestCanary_WithDefault_ExpectDefault() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}
	[Fact]
	public void Ray_WithDefault_ExpectDefault() {
		Point p = new Point(1, 2, 3);
		Vector d = new Vector(4, 5, 6);
		Ray r = new Ray(p, d);
		Assert.True(_fieldComp.CheckTuple(r._fieldOrigin,p));
		Assert.True(_fieldComp.CheckTuple(r._fieldDirection,d));
	}
	[Fact]
	public void RayGetPosition_WithAfterTime_ExpectMovement() {
		Point p = new Point(2, 3, 4);
		Vector d = new Vector(1, 0, 0);
		Ray r = new Ray(p, d);
		SpaceTuple pos0 = r.GetPosition(0);
		Point expos0 = new Point(2, 3, 4);
		SpaceTuple pos1 = r.GetPosition(1);
		Point expos1 = new Point(3, 3, 4);
		SpaceTuple posn1 = r.GetPosition(-1);
		Point exposn1 = new Point(1, 3, 4);
		SpaceTuple pos2p5 = r.GetPosition(2.5);
		Point expos2p5 = new Point(4.5, 3, 4);
		Assert.True(_fieldComp.CheckTuple(pos0,expos0));
		Assert.True(_fieldComp.CheckTuple(pos1, expos1));
		Assert.True(_fieldComp.CheckTuple(posn1, exposn1));
		Assert.True(_fieldComp.CheckTuple(pos2p5, expos2p5));
	}
	[Fact]
	public void RayGetTransformation_WithTranslation_ExpectTranslated() {
		Point p = new Point(1, 2, 3);
		Vector d = new Vector(0, 1, 0);
		Ray r = new Ray(p, d);
		TranslationMatrix m = new TranslationMatrix(3, 4, 5);
		Point expectedP = new Point(4, 6, 8);
		Vector expectedV = new Vector(0,1,0);
		Ray t = r.GetTransformed(m);
		Assert.True(_fieldComp.CheckTuple(t._fieldOrigin, expectedP));
		Assert.True(_fieldComp.CheckTuple(t._fieldDirection, expectedV));
		Assert.True(_fieldComp.CheckTuple(r._fieldOrigin, p));
		Assert.True(_fieldComp.CheckTuple(r._fieldDirection, d));
		r.ChangeTransform(m);
		Assert.True(_fieldComp.CheckTuple(r._fieldOrigin, expectedP));
		Assert.True(_fieldComp.CheckTuple(r._fieldDirection, expectedV));
	}
	[Fact]
	public void RayGetTransformation_WithScaling_ExpectScaledRay() {
		Point p = new Point(1, 2, 3);
		Vector d = new Vector(0, 1, 0);
		Ray r = new Ray(p, d);
		ScalingMatrix m = new ScalingMatrix(2, 3, 4);
		Point expectedP = new Point(2, 6, 12);
		Vector expectedV = new Vector(0,3,0);
		Ray t = r.GetTransformed(m);
		Assert.True(_fieldComp.CheckTuple(t._fieldOrigin, expectedP));
		Assert.True(_fieldComp.CheckTuple(t._fieldDirection, expectedV));
		Assert.True(_fieldComp.CheckTuple(r._fieldOrigin, p));
		Assert.True(_fieldComp.CheckTuple(r._fieldDirection, d));
		r.ChangeTransform(m);
		Assert.True(_fieldComp.CheckTuple(r._fieldOrigin, expectedP));
		Assert.True(_fieldComp.CheckTuple(r._fieldDirection, expectedV));
	}
}