export default function nameof<T>(key: keyof T, instance?: T): keyof T {
	return key;
}
