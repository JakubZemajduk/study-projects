import SwiftUI
import MapKit

struct MapView: View {
    var currencyCode: String
    @State private var region = MKCoordinateRegion(
        center: CLLocationCoordinate2D(latitude: 0.0, longitude: 0.0),
        span: MKCoordinateSpan(latitudeDelta: 0.5, longitudeDelta: 0.5)
    )
    
    //Koordynaty dla kodów walut
    private let coordinates: [String: CLLocationCoordinate2D] = [
        "USD": CLLocationCoordinate2D(latitude: 38.9072, longitude: -77.0369), // Washington, D.C.
        "EUR": CLLocationCoordinate2D(latitude: 48.8566, longitude: 2.3522),   // Paris
        "PLN": CLLocationCoordinate2D(latitude: 52.2297, longitude: 21.0122), // Warsaw
        "GBP": CLLocationCoordinate2D(latitude: 51.5074, longitude: -0.1278), // London
        
    ]
    
    var body: some View {
        Map(coordinateRegion: $region)
            .edgesIgnoringSafeArea(.all)
            .onAppear {
                setRegion(for: currencyCode)
            }
            .navigationTitle("Capital of: \(currencyCode)")
    }
    
    private func setRegion(for currencyCode: String) {
        if let coordinate = coordinates[currencyCode] {
            region = MKCoordinateRegion(
                center: coordinate,
                span: MKCoordinateSpan(latitudeDelta: 0.5, longitudeDelta: 0.5)
            )
        } else {
            print("No coordinates available for \(currencyCode)")
        }
    }
}
