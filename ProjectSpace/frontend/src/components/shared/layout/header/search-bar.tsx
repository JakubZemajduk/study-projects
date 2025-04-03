'use client';

import { type ChangeEvent, useEffect, useState } from 'react';
import { Search } from 'lucide-react';

import { useRouter, useSearchParams } from 'next/navigation';

import { Input } from '@/components/ui/input';
import { useDebounce } from '@/hooks/use-debounce';

export const SearchBar = () => {
	const router = useRouter();
	const searchParams = useSearchParams();
	const [searchValue, setSearchValue] = useState(searchParams.get('q') ?? '');
	const debouncedValue = useDebounce(searchValue, 500);

	const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
		setSearchValue(e.target.value);
	};

	useEffect(() => {
		if (debouncedValue.trim().length > 2) {
			router.push(`/search?query=${debouncedValue}`);
		}
	}, [router, debouncedValue]);

	return (
		<div className="relative flex max-w-[364px] flex-1 items-center">
			<Search className="absolute left-3 size-4 text-neutral-400" aria-hidden="true" />
			<Input
				id="search"
				aria-label="Search for a song, playlist or artist"
				type="search"
				placeholder="What do you want to listen to?"
				className="size-full rounded-full bg-slate-800/50 pl-10"
				onChange={handleChange}
				value={searchValue}
			/>
		</div>
	);
};
