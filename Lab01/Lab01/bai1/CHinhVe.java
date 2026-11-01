package bai1;
public abstract class CHinhVe {
    protected int maLoaiHinhVe;
    public CHinhVe(int maLoaiHinhVe) {
        this.maLoaiHinhVe = maLoaiHinhVe;
    }
	public double dienTich() {
        return 0;
    }
    public double chuVi() {
        return 0;
    }
    public void Ve() {
        return;
    }
    @Override
    public String toString() {
        return String.format("Hình: %d, Diện Tích: %.2f, Chu Vi: %.2f", maLoaiHinhVe, dienTich(), chuVi());
    }
}
