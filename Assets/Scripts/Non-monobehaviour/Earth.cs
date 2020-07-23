public class Earth : KinematicEquation {

   public static readonly string equation = "Vf = Vi + aΔt";
   public static readonly bool[] presentQuantities = {true, true, true, true, false};

   public Earth(bool[] knownQuantities, string[] quantities) {
    absentQuantityIndex = 4;
    SetKnownQuantities(knownQuantities);
    SetQuantities(quantities);
        
    SetLeftSide(new UnaryExpression(GetQuantity(1)));
    SetRightSide(new BinaryExpression(new UnaryExpression(GetQuantity(0)),
            new BinaryExpression(new UnaryExpression(GetQuantity(3)), new UnaryExpression(GetQuantity(2)), '*'), '+'));

    CheckNumberOfQuantities(NumberOfKnownQuantities());
    }

override
   public void DoAlgebra() {
    SetWork(new Steps(equation));
    if (IsTimeKnown()) {
        Algebra.solveEquation(false, this.work, this.leftSide, this.rightSide, GetMissingQuantityIndex());
        }
        else {
            Algebra.solveEquation(true, this.work, this.leftSide, this.rightSide, GetMissingQuantityIndex());
        }

    SetQuantity(GetMissingQuantityIndex(), float.ToString(this.work.GetNumericalAnswer())); //adds answer to array and updates knowns
    }
}
