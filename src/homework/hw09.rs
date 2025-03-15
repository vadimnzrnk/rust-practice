fn rotate(s: String, n: isize) -> String {
    let len = s.len();
    if len == 0 {
        return s;
    }

    let n = ((n % len as isize) + len as isize) % len as isize; // Нормалізація зсуву
    let split_point = len - n as usize;

    let (left, right) = s.split_at(split_point);
    format!("{}{}", right, left)
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_rotate() {
        let s = "abcdefgh".to_string();
        let shifts = [
            (0, "abcdefgh"),
            (8, "abcdefgh"),
            (-8, "abcdefgh"),
            (1, "habcdefg"),
            (2, "ghabcdef"),
            (10, "ghabcdef"),
            (-1, "bcdefgha"),
            (-2, "cdefghab"),
            (-10, "cdefghab"),
        ];

        for (n, exp) in shifts.iter() {
            assert_eq!(rotate(s.clone(), *n), exp.to_string());
        }
    }
}
