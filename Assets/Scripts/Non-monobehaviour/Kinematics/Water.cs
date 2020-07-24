public class Water : KinematicEquation {

   public static readonly string equation = "ΔX = 1/2(Vf + Vi)Δt";
   public static readonly bool[] presentQuantities = {true, true, true, false, true};

   public Water(bool[] knownQuantities, string[] quantities) {
        absentQuantityIndex = 3;
        SetKnownQuantities(knownQuantities);
        SetQuantities(quantities);

        SetLeftSide(new UnaryExpression(GetQuantity(4)));
        SetRightSide(new BinaryExpression(new BinaryExpression(new BinaryExpression(new UnaryExpression(GetQuantity(1)), new UnaryExpression(GetQuantity(0)), '+'), new UnaryExpression("0.5"), '*'), new UnaryExpression(GetQuantity(2)), '*'));

        CheckNumberOfQuantities(NumberOfKnownQuantities());
   }

   override
   public void DoAlgebra() {
        SetWork(new Steps(equation));
        if (IsTimeKnown()) {
            Algebra.solveEquation(false, this.work, leftSide, rightSide, GetMissingQuantityIndex());
        } else {
            Algebra.solveEquation(true, this.work, leftSide, rightSide, GetMissingQuantityIndex());
        }

        SetQuantity(GetMissingQuantityIndex(), this.work.GetNumericalAnswer().ToString()); //adds answer to array and updates knowns

   }
}
