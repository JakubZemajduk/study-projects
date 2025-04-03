import Link from 'next/link';

import { Avatar, AvatarImage } from '@/components/ui/avatar';
import {
	DropdownMenu,
	DropdownMenuContent,
	DropdownMenuItem,
	DropdownMenuSeparator,
	DropdownMenuTrigger,
} from '@/components/ui/dropdown-menu';

import { SearchBar } from './search-bar';

export const Header = () => {
	return (
		<header className="sticky top-0 z-50 flex h-16 items-center justify-between gap-x-4 bg-slate-950 px-4">
			<Link href="/">
				<h1 className="text-xl font-semibold text-brand md:text-3xl">.NETify</h1>
			</Link>
			<SearchBar />
			<DropdownMenu>
				<DropdownMenuTrigger>
					<Avatar>
						<AvatarImage src="/album.jpg" alt="User avatar" />
					</Avatar>
				</DropdownMenuTrigger>
				<DropdownMenuContent align="end" className="w-56">
					<DropdownMenuItem>Account</DropdownMenuItem>
					<DropdownMenuItem>Profile</DropdownMenuItem>
					<DropdownMenuItem>Settings</DropdownMenuItem>
					<DropdownMenuSeparator />
					<DropdownMenuItem>Log out</DropdownMenuItem>
				</DropdownMenuContent>
			</DropdownMenu>
		</header>
	);
};
