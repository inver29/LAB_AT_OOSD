package bai1;

public class CTuGiac extends CHinhVe {
    private final CDiem diemP1;
    private final CDiem diemP2;
    private final CDiem diemP3;
    private final CDiem diemP4;

    public CTuGiac(CDiem diemP1, CDiem diemP2, CDiem diemP3, CDiem diemP4) {
        super(2);
        this.diemP1 = diemP1;
        this.diemP2 = diemP2;
        this.diemP3 = diemP3;
        this.diemP4 = diemP4;
    }

    @Override
    public double dienTich() {
        double tong = diemP1.getX() * diemP2.getY() - diemP1.getY() * diemP2.getX()
                + diemP2.getX() * diemP3.getY() - diemP2.getY() * diemP3.getX()
                + diemP3.getX() * diemP4.getY() - diemP3.getY() * diemP4.getX()
                + diemP4.getX() * diemP1.getY() - diemP4.getY() * diemP1.getX();
        return Math.abs(tong) / 2;
    }
    
    @Override
    public double chuVi() {
        return diemP1.khoangCachDen(diemP2)
                + diemP2.khoangCachDen(diemP3)
                + diemP3.khoangCachDen(diemP4)
                + diemP4.khoangCachDen(diemP1);
    }
}
