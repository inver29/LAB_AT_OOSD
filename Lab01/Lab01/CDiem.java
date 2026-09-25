package bai1;

public class CDiem {
    private final double x;
    private final double y;

    public CDiem(double x, double y) {
        this.x = x;
        this.y = y;
    }

    public double getX() {
        return x;
    }

    public double getY() {
        return y;
    }

    public double khoangCachDen(CDiem diemKhac) {
        return Math.hypot(x - diemKhac.x, y - diemKhac.y);
    }
}
