import type { Metadata } from 'next';
import { Figtree } from 'next/font/google';

import { AudioPlayer } from '@/components/shared/audio-player';
import { Header } from '@/components/shared/layout/header';
import { Sidebar } from '@/components/shared/layout/sidebar';
import { PlaybackControls } from '@/components/shared/playback-controls';
import { cn } from '@/lib/utils';

import './globals.css';
const figtree = Figtree({
	subsets: ['latin'],
	display: 'swap',
	weight: ['300', '400', '500', '600', '700', '800', '900'],
	variable: '--font-figtree',
});

export const metadata: Metadata = {
	title: '.NETify',
	description: 'The best Spotify clone',
};

export default function RootLayout({ children }: { children: React.ReactNode }) {
	return (
		<html lang="en">
			<body
				className={cn(
					'flex min-h-screen flex-col overflow-x-hidden bg-slate-950 antialiased',
					figtree.className,
				)}
			>
				<AudioPlayer />
				<Header />
				<div className="flex flex-1 gap-x-2">
					<aside className="sticky top-16 flex h-screen w-1/6 max-w-xs flex-col gap-2 md:w-1/5">
						<Sidebar />
					</aside>
					<main className="w-5/6 md:w-4/5">{children}</main>
				</div>
				<PlaybackControls />
			</body>
		</html>
	);
}
