import axios from 'axios';
import { assert, expect } from 'chai';

describe('Property Search API Testing', function () {
  this.timeout(5000);
  
  // Reusing the property validation function from properties.test.js
  function validateProperty(property) {
    // expect(property).to.have.property('amenities').that.is.a('string');
    expect(property).to.have.property('available_date').that.is.a('string');
    expect(property).to.have.property('bathrooms').that.is.a('number');
    expect(property).to.have.property('bedrooms').that.is.a('number');
    expect(property).to.have.property('created_date').that.is.a('string');
    expect(property).to.have.property('created_user').that.is.a('number');
    expect(property).to.have.property('currency').that.is.a('string');
    expect(property).to.have.property('description').that.is.a('string');
    expect(property).to.have.property('hero_image').that.is.a('string');
    expect(property).to.have.property('is_bookmarked').that.is.a('boolean');
    expect(property).to.have.property('images').that.is.an('array');
    expect(property).to.have.property('modified_date').that.is.a('string');
    expect(property).to.have.property('modified_user').that.is.a('number');
    expect(property).to.have.property('price').that.is.a('number');
    expect(property).to.have.property('property_id').that.is.a('number');
    expect(property).to.have.property('property_state').that.is.a('string');
    expect(property).to.have.property('property_status').that.is.a('string');
    expect(property).to.have.property('property_type').that.is.a('string');
    expect(property).to.have.property('rent_type').that.is.a('string');
    expect(property).to.have.property('size').that.is.a('string');
    expect(property).to.have.property('title').that.is.a('string');
    
    // Rating properties - now nested
    expect(property).to.have.property('rating').that.is.an('object');
    expect(property.rating).to.have.property('cleanliness').that.is.a('number');
    expect(property.rating).to.have.property('traffic').that.is.a('number');
    expect(property.rating).to.have.property('amenities').that.is.a('number');
    expect(property.rating).to.have.property('safety').that.is.a('number');
    expect(property.rating).to.have.property('value_for_money').that.is.a('number');
    expect(property.rating).to.have.property('total').that.is.a('number');
  
    expect(property).to.have.property('address');
    expect(property.address).to.have.property('block_no').that.is.a('string');
    expect(property.address).to.have.property('postal_code').that.is.a('string');
    expect(property.address).to.have.property('region').that.is.a('string');
    expect(property.address).to.have.property('street').that.is.a('string');
    expect(property.address).to.have.property('unit_no').that.is.a('string');
  
    expect(property).to.have.property('user');
    expect(property.user).to.have.property('user_id').that.is.a('number');
    expect(property.user).to.have.property('email').that.is.a('string');
    expect(property.user).to.have.property('first_name').that.is.a('string');
    expect(property.user).to.have.property('last_name').that.is.a('string');
    expect(property.user).to.have.property('profile_image').that.is.a('string');
    expect(property.user).to.have.property('contact_number').that.is.a('string');
    expect(property.user).to.have.property('company').that.is.a('string');
    expect(property.user).to.have.property('property_role').that.is.a('string');
  }

  it('should successfully search properties by title', async () => {
    const searchTerm = 'Pasir Ris';
    const response = await axios.get(`http://localhost:5000/api/v2/property/search?title=${searchTerm}`);
    
    expect(response.status).to.equal(200);
    
    const properties = response.data;
    expect(properties).to.be.an('array');
    expect(properties).to.be.not.empty;
    
    // Validate that all returned properties contain the search term in their title
    for (const property of properties) {
      expect(property.title.toLowerCase()).to.include(searchTerm.toLowerCase());
      validateProperty(property);
    }
  });

  it('should return empty array when searching for non-existent property title', async () => {
    // Search for a term that should not exist in property titles
    const searchTerm = 'nonexistentpropertytitle12345';
    const response = await axios.get(`http://localhost:5000/api/v2/property/search?title=${searchTerm}`);
    
    expect(response.status).to.equal(200);
    
    const properties = response.data;
    expect(properties).to.be.an('array');
    expect(properties).to.be.empty;
  });

  it('should return 400 Bad Request when searching with empty title', async () => {
    try {
      await axios.get('http://localhost:5000/api/v2/property/search?title=');
      assert.fail('Expected error not thrown');
    } catch (error) {
      expect(error.response.status).to.equal(400);
    }
  });
});