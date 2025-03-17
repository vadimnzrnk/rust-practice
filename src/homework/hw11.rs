use rand::Rng;

// Генерує випадковий вектор довжиною n зі значеннями [10..99]
fn gen_random_vector(n: usize) -> Vec<i32> {
    let mut rng = rand::thread_rng();
    (0..n).map(|_| rng.gen_range(10..100)).collect()
}

// Знаходить мінімальну суму сусідніх елементів у векторі
fn min_adjacent_sum(data: &[i32]) -> Option<(i32, i32, i32)> {
    if data.len() < 2 {
        return None;
    }
    
    let (mut min_sum, mut min_pair) = (i32::MAX, (0, 0));
    
    for i in 0..data.len() - 1 {
        let sum = data[i] + data[i + 1];
        if sum < min_sum {
            min_sum = sum;
            min_pair = (data[i], data[i + 1]);
        }
    }

    Some((min_pair.0, min_pair.1, min_sum))
}

// Виводить інформацію у зрозумілому вигляді
fn print_result(data: &[i32]) {
    println!("Згенерований вектор: {:?}", data);
    
    match min_adjacent_sum(data) {
        Some((a, b, sum)) => println!("Мінімальна сума пари: {} + {} = {}", a, b, sum),
        None => println!("У векторі недостатньо елементів для пошуку пари."),
    }
}

fn main() {
    let vec = gen_random_vector(20);
    print_result(&vec);
}
