//
//  CurrencyService.swift
//  ProjektJZ
//
//  Created by student on 15/01/2025.
//

import Foundation

class CurrencyService{
    static let shared = CurrencyService()
    private let baseURL = "https://api.nbp.pl/api/exchangerates/tables/A/"
    
    private init() {}
    func fetchCurrencies(completion: @escaping([Currency]?) -> Void){
        guard let url = URL(string: baseURL) else{
            completion(nil)
            return
        }
        URLSession.shared.dataTask(with: url) {data, _, error in
            if let error = error {
                print("Error przy pobieraniu danych: \(error.localizedDescription)")
                completion(nil)
                return
            }
            guard let data = data else {
                completion(nil)
                return
            }
            do {
                let currencyTables = try JSONDecoder().decode([CurrencyTable].self, from: data)
                completion(currencyTables.first?.rates)
            } catch {
                print("Error decoding Json: \(error.localizedDescription)")
                completion(nil)
            }
        }.resume()
    }
}

