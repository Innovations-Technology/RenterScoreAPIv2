import { expect } from 'chai';
import axios from 'axios';

describe('Rating API Tests', () => {
    const baseUrl = 'http://localhost:5000/api/v2/rating';
    
    describe('POST /rating', () => {
        it('should add a new rating successfully', async () => {
            const ratingData = {
                user_id: 44,
                property_id: 7,
                cleanliness: 4,
                traffic: 3,
                amenities: 5,
                safety: 4,
                value_for_money: 3
            };

            const response = await axios.post(baseUrl, ratingData);
            
            expect(response.status).to.equal(200);
            expect(response.data.message).to.equal('Rating added successfully');
        });

        it('should update existing rating', async () => {
            const ratingData = {
                user_id: 44,
                property_id: 7,
                cleanliness: 5,
                traffic: 4,
                amenities: 4,
                safety: 5,
                value_for_money: 4
            };

            const response = await axios.post(baseUrl, ratingData);
            
            expect(response.status).to.equal(200);
            expect(response.data.message).to.equal('Rating added successfully');
        });

        it('should return 404 for non-existent property', async () => {
            const ratingData = {
                user_id: 1,
                property_id: 999999,
                cleanliness: 4,
                traffic: 3,
                amenities: 5,
                safety: 4,
                value_for_money: 3
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
}); 