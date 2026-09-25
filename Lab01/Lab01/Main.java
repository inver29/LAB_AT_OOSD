package bai1;

public class Main {
    public static void main(String[] args) {
        CDiem a = new CDiem(0, 0);
        CDiem b = new CDiem(4, 0);
        CDiem c = new CDiem(0, 3);
        CHinhVe tamGiac = new CTamGiac(a, b, c);

        CDiem d = new CDiem(4, 3);
        CHinhVe tuGiac = new CTuGiac(a, b, d, c);

        CHinhVe elipse = new CElipse(new CDiem(0, 0), 5, 3);

        System.out.println(tamGiac);
        System.out.println(tuGiac);
        System.out.println(elipse);
    }
}
