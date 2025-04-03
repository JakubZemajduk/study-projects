import { type ClassValue, clsx } from 'clsx';
import { twMerge } from 'tailwind-merge';

export function cn(...inputs: ClassValue[]) {
	return twMerge(clsx(inputs));
}

export const formatTime = (seconds: number) => {
	const minutes = Math.floor(seconds / 60);
	const remainingSeconds = Math.floor(seconds % 60);
	return `${minutes}:${remainingSeconds.toString().padStart(2, '0')}`;
};

export function invariant(condition: boolean, message: string): asserts condition {
	if (!condition) throw new Error(message);
}

export function assertNonNull<T>(value: T, message: string): asserts value is NonNullable<T> {
	if (value === null || value === undefined) throw new Error(message);
}
