package bai1;

public class CTamGiac extends CHinhVe {
    private final CDiem diemP1;
    private final CDiem diemP2;
    private final CDiem diemP3;

    public CTamGiac(CDiem diemP1, CDiem diemP2, CDiem diemP3) {
        super(1);
        this.diemP1 = diemP1;
        this.diemP2 = diemP2;
        this.diemP3 = diemP3;
    }

    @Override
    public double dienTich() {
        double nuaChuVi = chuVi() / 2;
        double canh12 = diemP1.khoangCachDen(diemP2);
        double canh23 = diemP2.khoangCachDen(diemP3);
        double canh31 = diemP3.khoangCachDen(diemP1);
        return Math.sqrt(nuaChuVi * (nuaChuVi - canh12)
                * (nuaChuVi - canh23) * (nuaChuVi - canh31));
    }

    @Override
    public double chuVi() {
        return diemP1.khoangCachDen(diemP2)
                + diemP2.khoangCachDen(diemP3)
                + diemP3.khoangCachDen(diemP1);
    }
}
