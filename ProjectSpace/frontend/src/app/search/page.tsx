interface Props {
  searchParams: {
    query: string;
  }
}

export default function SearchPage({ searchParams }: Props) {
  const query = searchParams.query;

  if (!query) {
    return <div>No query provided</div>;
  }

  return <h1 className="text-2xl font-bold text-center">{ query }</h1>;
}

