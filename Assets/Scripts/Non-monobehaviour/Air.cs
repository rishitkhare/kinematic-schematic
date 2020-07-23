public class Air : KinematicEquation {

   public static readonly string equation = "ΔX = ViΔt + 1/2aΔt^2";
   public static readonly bool[] presentQuantities = {true, false, true, true, true};

   public Air(bool[] knownQuantities, string[] quantities) {
    absentQuantityIndex = 1;
    SetKnownQuantities(knownQuantities);
    SetQuantities(quantities);

    SetLeftSide(new UnaryExpression(GetQuantity(4)));
    SetRightSide(new BinaryExpression(new BinaryExpression(new UnaryExpression(GetQuantity(0)), new UnaryExpression(GetQuantity(2)), '*'), new BinaryExpression(new BinaryExpression("0.5", new UnaryExpression(GetQuantity(3)), '*'), new BinaryExpression(new UnaryExpression(GetQuantity(2)), "2", '^'), '*'), '+'));

    CheckNumberOfQuantities(NumberOfKnownQuantities());
    }

override
   public void DoAlgebra() {

    SetWork(new Steps(equation));
    if (!IsTimeKnown()) {
        Algebra.getPositiveQuadraticRoot(work, 0.5 * GetNumericalQuantity(3), GetNumericalQuantity(0), -1 * GetNumericalQuantity(4));
        } else {
        Algebra.solveEquation(false, work, leftSide, rightSide, GetMissingQuantityIndex());
        }

    SetQuantity(GetMissingQuantityIndex(), float.ToString(this.work.GetNumericalAnswer())); //adds answer to array and updates knowns
    }
}
