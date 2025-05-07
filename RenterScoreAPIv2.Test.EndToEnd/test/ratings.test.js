import { expect } from 'chai';
import axios from 'axios';

describe('Rating API Tests', () => {
    const baseUrl = 'http://localhost:5000/api/v2/rating';
    
    describe('POST /rating', () => {
        it('should add a new rating successfully', async () => {
            const ratingData = {
                userId: 44,
                propertyId: 7,
                cleanliness: 4,
                traffic: 3,
                amenities: 5,
                safety: 4,
                valueForMoney: 3
            };

            const response = await axios.post(baseUrl, ratingData);
            
            expect(response.status).to.equal(200);
            expect(response.data).to.be.an('object');
            validateRating(response.data);
        });

        it('should update existing rating', async () => {
            const ratingData = {
                userId: 44,
                propertyId: 7,
                cleanliness: 5,
                traffic: 4,
                amenities: 4,
                safety: 5,
                valueForMoney: 4
            };

            const response = await axios.post(baseUrl, ratingData);
            
            expect(response.status).to.equal(200);
            expect(response.data).to.be.an('object');
            validateRating(response.data);
            expect(response.data.cleanliness).to.equal(5);
            expect(response.data.traffic).to.equal(4);
        });

        it('should return 404 for non-existent property', async () => {
            const ratingData = {
                userId: 1,
                propertyId: 999999,
                cleanliness: 4,
                traffic: 3,
                amenities: 5,
                safety: 4,
                valueForMoney: 3
            };

            try {
                await axios.post(baseUrl, ratingData);
                expect.fail('Should have thrown an error');
            } catch (error) {
                expect(error.response.status).to.equal(404);
                expect(error.response.data).to.equal('Property not found');
            }
        });
    });

    describe('GET /rating', () => {
        it('should get rating for existing user and property', async () => {
            const response = await axios.get(`${baseUrl}?userId=44&propertyId=7`);
            
            expect(response.status).to.equal(200);
            expect(response.data).to.be.an('object');
            validateRating(response.data);
        });

        it('should return "No rating found" for non-existent rating', async () => {
            try {
                await axios.get(`${baseUrl}?userId=999999&propertyId=999999`);
                expect.fail('Should have thrown an error');
            } catch (error) {
                expect(error.response.status).to.equal(404);
                expect(error.response.data).to.equal('No rating found');
            }
        });
    });
});

function validateRating(rating) {
    expect(rating).to.have.property('cleanliness').that.is.a('number');
    expect(rating).to.have.property('traffic').that.is.a('number');
    expect(rating).to.have.property('amenities').that.is.a('number');
    expect(rating).to.have.property('safety').that.is.a('number');
    expect(rating).to.have.property('valueForMoney').that.is.a('number');
    expect(rating).to.have.property('total').that.is.a('number');
    
    // Validate rating ranges
    expect(rating.cleanliness).to.be.at.least(1).and.at.most(5);
    expect(rating.traffic).to.be.at.least(1).and.at.most(5);
    expect(rating.amenities).to.be.at.least(1).and.at.most(5);
    expect(rating.safety).to.be.at.least(1).and.at.most(5);
    expect(rating.valueForMoney).to.be.at.least(1).and.at.most(5);
    expect(rating.total).to.be.at.least(1).and.at.most(5);
} 