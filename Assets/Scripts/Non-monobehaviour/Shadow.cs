public class Shadow : KinematicEquation {

   public static readonly string equation = "ΔX = VfΔt - 1/2aΔt^2";
   public static readonly bool[] presentQuantities = {false, true, true, true, true};

   public Shadow(bool[] knownQuantities, string[] quantities) {
    absentQuantityIndex = 0;
    SetKnownQuantities(knownQuantities);
    SetQuantities(quantities);

    SetLeftSide(new UnaryExpression(GetQuantity(4)));
    SetRightSide(new BinaryExpression(new BinaryExpression(new UnaryExpression(GetQuantity(1)), new UnaryExpression(GetQuantity(2)), '*'), new BinaryExpression(new BinaryExpression("0.5", new UnaryExpression(GetQuantity(3)), '*'), new BinaryExpression(new UnaryExpression(GetQuantity(2)), "2", '^'), '*'), '-'));

    CheckNumberOfQuantities(NumberOfKnownQuantities());
    }

override
public void DoAlgebra() {
    Expression leftSide = new UnaryExpression(GetQuantity(4));
    Expression rightSide = new BinaryExpression(new BinaryExpression(new UnaryExpression(GetQuantity(1)), new UnaryExpression(GetQuantity(2)), '*'), new BinaryExpression(new BinaryExpression("0.5", new UnaryExpression(GetQuantity(3)), '*'), new BinaryExpression(new UnaryExpression(GetQuantity(2)), "2", '^'), '*'), '-');
    SetWork(new Steps(equation));
    if (!IsTimeKnown()) {
        Algebra.getPositiveQuadraticRoot(work, -0.5f * GetNumericalQuantity(3), GetNumericalQuantity(1), -1 * GetNumericalQuantity(4));
    } else {
        Algebra.solveEquation(false, this.work, leftSide, rightSide, GetMissingQuantityIndex());
    }

    SetQuantity(GetMissingQuantityIndex(), this.work.GetNumericalAnswer().ToString()); //adds answer to array and updates knowns
    }
}
