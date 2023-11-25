using LibTuple;
namespace LibComparinator;

public class Comparinator {
    public double epsilon = .0001;
    public bool CheckFloat(double argX, double argY) {
        return Math.Abs(argX-argY) < epsilon;
    }
    public bool CheckTuple(SpaceTuple argX, SpaceTuple argY) {
        return CheckFloat(argX._fieldX, argY._fieldX) && CheckFloat(argX._fieldY, argY._fieldY) && CheckFloat(argX._fieldZ, argY._fieldZ) && CheckFloat(argX._fieldW, argY._fieldW);
    }
}