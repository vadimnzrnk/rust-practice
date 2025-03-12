fn draw_rhombus(size: usize) {
    if size % 2 == 0 {
        println!("Розмір має бути непарним числом!");
        return;
    }

    let mid = size / 2;

    for i in 0..size {
        let spaces = if i <= mid { mid - i } else { i - mid };
        let stars = if i <= mid { 2 * i + 1 } else { 2 * (size - i - 1) + 1 };

        println!("{}{}", " ".repeat(spaces), "*".repeat(stars));
    }
}

fn main() {
    let size = 7; // Виберіть непарний розмір ромба
    draw_rhombus(size);
}
