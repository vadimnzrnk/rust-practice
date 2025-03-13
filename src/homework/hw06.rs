fn draw_christmas_tree() {
    let levels = 6; // Кількість рівнів у ялинці
    let max_width = 11; // Максимальна ширина нижнього рівня
    
    for level in 1..=levels {
        for row in 0..level {
            let stars = 2 * row + 1;
            let spaces = (max_width - stars) / 2;
            println!("{}{}", " ".repeat(spaces), "*".repeat(stars));
        }
    }
    
    // Малюємо стовбур
    for _ in 0..2 {
        let spaces = (max_width - 1) / 2;
        println!("{}*", " ".repeat(spaces));
    }
}

fn main() {
    draw_christmas_tree();
}
