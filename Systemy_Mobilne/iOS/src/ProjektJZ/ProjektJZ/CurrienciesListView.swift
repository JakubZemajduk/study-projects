import SwiftUI

struct CurrenciesListView: View {
    @State private var currencies: [Currency] = []
    
    var body: some View {
        VStack {
            if currencies.isEmpty {
                ProgressView("Loading...")
                    .onAppear {
                        fetchCurrenciesFromNBP()
                    }
            } else {
                List(currencies, id: \.code) { currency in
                    HStack {
                        Text("\(currency.currency) (\(currency.code))")
                        Spacer()
                        Text(String(format: "%.2f", currency.mid))
                            .foregroundColor(.blue)
                    }
                }
                .listStyle(PlainListStyle())
                .navigationTitle("Currencies and Rates")
            }
        }
    }
    
    private func fetchCurrenciesFromNBP() {
        CurrencyService.shared.fetchCurrencies { fetchedCurrencies in
            DispatchQueue.main.async {
                self.currencies = fetchedCurrencies ?? []
            }
        }
    }
}
