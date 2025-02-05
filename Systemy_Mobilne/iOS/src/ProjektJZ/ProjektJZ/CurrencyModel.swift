//
//  CurrencyModel.swift
//  ProjektJZ
//
//  Created by student on 15/01/2025.
//

import Foundation

struct CurrencyTable: Codable{
    let rates: [Currency]
}

struct Currency: Codable, Hashable{
    let code: String
    let currency: String
    let mid: Double
}
