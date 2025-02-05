//
//  ContentView.swift
//  ProjektJZ
//
//  Created by student on 15/01/2025.
//

import SwiftUI

extension UIWindow {
    open override func motionEnded(_ motion: UIEvent.EventSubtype, with event: UIEvent?) {
        if motion == .motionShake {
            NotificationCenter.default.post(name: .deviceShaken, object: nil)
        }
    }
}

extension Notification.Name {
    static let deviceShaken = Notification.Name("deviceShaken")
}

struct ContentView: View {
    @State private var currencies: [Currency] = []
    @State private var selectedFromCurrency: Currency?
    @State private var selectedToCurrency: Currency?
    @State private var amount: String = ""
    @State private var result: String = ""
    
    var body: some View {
        NavigationView {
            VStack(spacing: 20) {
                Text("From:")
                    .font(.headline)
                    .frame(maxWidth: .infinity, alignment: .leading)
                    .padding(.leading)
                
                Picker("From Currency", selection: $selectedFromCurrency) {
                    ForEach(currencies, id: \.code) { currency in
                        Text("\(currency.currency) (\(currency.code))").tag(currency as Currency?)
                    }
                }
                .pickerStyle(MenuPickerStyle())
                
                Text("To:")
                    .font(.headline)
                    .frame(maxWidth: .infinity, alignment: .leading)
                    .padding(.leading)
                
                Picker("To Currency", selection: $selectedToCurrency) {
                    ForEach(currencies, id: \.code) { currency in
                        Text("\(currency.currency) (\(currency.code))").tag(currency as Currency?)
                    }
                }
                .pickerStyle(MenuPickerStyle())
                
                TextField("Amount", text: $amount)
                    .keyboardType(.decimalPad)
                    .padding()
                    .background(Color.gray.opacity(0.2))
                    .cornerRadius(8)
                
                Button(action: convertCurrency) {
                    Text("Convert")
                        .font(.headline)
                        .frame(maxWidth: .infinity)
                        .padding()
                        .background(LinearGradient(gradient: Gradient(colors: [Color.blue, Color.purple]), startPoint: .leading, endPoint: .trailing))
                        .foregroundColor(.white)
                        .cornerRadius(10)
                }
                
                Text(result)
                    .font(.headline)
                    .padding()
                
                if let selectedCurrency = selectedFromCurrency {
                    NavigationLink(destination: MapView(currencyCode: selectedCurrency.code)) {
                        Text("View Capital of \(selectedCurrency.code)")
                            .font(.headline)
                            .frame(maxWidth: .infinity)
                            .padding()
                            .background(LinearGradient(gradient: Gradient(colors: [Color.green, Color.teal]), startPoint: .leading, endPoint: .trailing))
                            .foregroundColor(.white)
                            .cornerRadius(10)
                    }
                }
                
                NavigationLink(destination: CurrenciesListView()) {
                    Text("View All Currencies and Rates")
                        .font(.headline)
                        .frame(maxWidth: .infinity)
                        .padding()
                        .background(LinearGradient(gradient: Gradient(colors: [Color.orange, Color.red]), startPoint: .leading, endPoint: .trailing))
                        .foregroundColor(.white)
                        .cornerRadius(10)
                }
            }
            .padding()
            .onAppear {
                fetchCurriences()
            }
            .onReceive(NotificationCenter.default.publisher(for: .deviceShaken)) { _ in
                resetAmount()
            }
        }
    }
    
    private func fetchCurriences() {
        CurrencyService.shared.fetchCurrencies { currencies in
            DispatchQueue.main.async {
                self.currencies = currencies ?? []
                if let firstCurrency = currencies?.first {
                    self.selectedFromCurrency = firstCurrency
                    self.selectedToCurrency = firstCurrency
                }
            }
        }
    }
    
    private func convertCurrency() {
        guard let fromCurrency = selectedFromCurrency,
              let toCurrency = selectedToCurrency,
              let amountDouble = Double(amount) else {
            result = "Invalid input"
            return
        }
        let fromRate = fromCurrency.mid
        let toRate = toCurrency.mid
        let convertedAmount = (amountDouble * fromRate) / toRate
        
        result = String(format: "%.2f \(fromCurrency.code) = %.2f \(toCurrency.code)", amountDouble, convertedAmount)
    }
    
    private func resetAmount() {
        amount = ""
        result = "Amount reset"
        if let firstCurrency = currencies.first {
            selectedFromCurrency = firstCurrency
            selectedToCurrency = firstCurrency
        }
    }
}
