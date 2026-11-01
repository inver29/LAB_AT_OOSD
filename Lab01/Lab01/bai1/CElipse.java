package bai1;

public class CElipse extends CHinhVe {
    protected CDiem cDiemTam;
    protected float a;
    protected float b;

    public CElipse(CDiem cDiemTam, float a, float b) {
        super(3);
        this.cDiemTam = cDiemTam;
        this.a = a;
        this.b = b;
    }

    @Override
    public double dienTich() {
        return Math.PI * a * b;
    }

    @Override
    public double chuVi() {
        return Math.PI * (3 * (a + b) - Math.sqrt((3 * a + b) * (a + 3 * b)));
    }
}
